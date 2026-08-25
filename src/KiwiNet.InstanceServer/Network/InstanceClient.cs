using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Math;
using KiwiNet.Core.Network.Tcp;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Commands;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Components;
using KiwiNet.InstanceServer.WorldAreas;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public class InstanceClient : TcpClient
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        // temp garbage
        private static InstanceClient _currentClient = null;
        private static string _areaOverride = null;
        private static Vector2Int? _startOverride = null;

        private string _characterName = string.Empty;
        private string _area;

        public InstanceClient()
        {
        }

        public override void OnDataReceived(byte[] buffer, int length)
        {
            //Logger.Debug($"OnDataReceived(): {Convert.ToHexString(buffer.AsSpan(0, length))}");

            List<Packet> packets = new();   // todo: pool this
            Packet.ParseFrom(buffer, length, packets);

            foreach (Packet packet in packets)
            {
                Logger.Trace($" IN < {packet.Id}");
                ReceivePacket(packet);
            }
        }

        public void Send(Packet packet)
        {
            Logger.Trace($"OUT > {packet.Id}");
            Connection.Send(packet);
        }

        public bool BeginAreaTransfer(string areaId, Vector2Int? startOverride = null)
        {
            if (WorldArea.IsValidAreaId(areaId) == false)
                return false;

            _areaOverride = areaId;
            _startOverride = startOverride;

            StringPacket notification = PacketFactory.Get<StringPacket>(PacketId.InstanceClientAreaChangeNotificationPacketId);
            notification.Value = areaId;
            Send(notification);

            InstanceClientInstanceDetailsPacket instanceDetails = PacketFactory.Get<InstanceClientInstanceDetailsPacket>();
            instanceDetails.SessionId = 0xDEADBEEF;
            instanceDetails.Field1 = 1;
            instanceDetails.WorldAreaId = areaId;
            instanceDetails.Entries.Add(new("localhost", "6112"));
            Send(instanceDetails);
            return true;
        }

        #region Handlers

        private void ReceivePacket(Packet packet)
        {
            switch (packet.Id)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    OnLoginAttempt(packet);
                    break;

                case PacketId.ClientInstanceChatMessagePacketId:
                    OnChatMessage(packet);
                    break;

                case PacketId.ClientInstanceHeartbeatPacketId:
                    OnHeartbeat();
                    break;

                case PacketId.ClientInstanceSkillTargetEntityId:
                    OnSkillTargetEntity(packet);
                    break;

                case PacketId.ClientInstanceSkillTargetLocationId:
                    OnSkillTargetLocation(packet);
                    break;

                case PacketId.ClientInstanceAllocatePassiveSkillPointPacketId:
                    OnAllocatePassiveSkillPoint(packet);
                    break;

                case PacketId.ClientInstanceChangeBoundSkillId:
                    OnChangeBoundSkill(packet);
                    break;

                case PacketId.ClientInstanceTerrainGenerationResultId:
                    OnTerrainGenerationResult(packet);
                    break;

                default:
                    Logger.Warn($"Unhandled packet [{(int)packet.Id}] {packet.Id}");
                    break;
            }
        }

        private void OnLoginAttempt(Packet packet)
        {
            if (packet is not ClientInstanceLoginAttemptPacket loginAttempt)
            {
                Logger.Warn("OnLoginAttempt(): Invalid packet");
                return;
            }

            Logger.Debug($"OnLoginAttempt(): {loginAttempt}");

            _characterName = loginAttempt.CharacterName;

            var reply = PacketFactory.Get<InstanceClientLoginAttemptReplyPacket>();
            reply.Field0 = 1;
            reply.Field1 = "";
            Send(reply);

            // temp garbage part deux
            _currentClient?.Connection.Disconnect();
            _currentClient = this;

            GameConfig config = ConfigManager.Get<GameConfig>();
            _area = _areaOverride ?? config.WorldAreaId;

            var instanceInfo = PacketFactory.Get<InstanceClientInstanceInformationPacket>();
            instanceInfo.Field0 = 1;
            instanceInfo.WorldAreaId = _area;
            instanceInfo.League = "Default";
            instanceInfo.Seed = (uint)config.WorldAreaSeed;
            Send(instanceInfo);
        }

        private void OnChatMessage(Packet packet)
        {
            if (packet is not ClientInstanceChatMessagePacket chatMessage)
            {
                Logger.Warn("OnChatMessage(): Invalid packet");
                return;
            }

            if (CommandManager.Instance.TryParseCommand(this, chatMessage.Text))
                return;

            Logger.Debug($"OnChatMessage(): {chatMessage.Text}");

            InstanceClientChatMessagePacket reply = PacketFactory.Get<InstanceClientChatMessagePacket>();
            reply.Name = _characterName;
            reply.Text = chatMessage.Text;
            Send(reply);
        }

        private void OnHeartbeat()
        {
            Send(PacketFactory.Get<Packet>(PacketId.InstanceClientHeartbeatReplyPacketId));
        }

        private void OnSkillTargetEntity(Packet packet)
        {
            Logger.Debug($"OnSkillTargetEntity(): {packet}");
        }

        private void OnSkillTargetLocation(Packet packet)
        {
            Logger.Debug($"OnSkillTargetLocation(): {packet}");
        }

        private void OnAllocatePassiveSkillPoint(Packet packet)
        {
            if (packet is not IntPacket allocatePassiveSkillPoint)
            {
                Logger.Warn("OnAllocatePassiveSkillPoint(): Invalid packet");
                return;
            }

            Logger.Debug($"OnAllocatePassiveSkillPoint(): 0x{allocatePassiveSkillPoint.Value:X8}");
        }

        private void OnChangeBoundSkill(Packet packet)
        {
            if (packet is not ClientInstanceChangeBoundSkill changeBoundSkill)
            {
                Logger.Warn("OnChangeBoundSkill(): Invalid packet");
                return;
            }

            Logger.Debug($"OnChangeBoundSkill(): {changeBoundSkill}");
        }

        private void OnTerrainGenerationResult(Packet packet)
        {
            if (packet is not ClientInstanceTerrainGenerationResult terrainGenerationResult)
            {
                Logger.Warn("OnTerrainGenerationResult(): Invalid packet");
                return;
            }

            Logger.Debug($"OnTerrainGenerationResult(): {terrainGenerationResult}");
            // this is where the server disconnects the client if the hashes don't match
            // InstanceClientForcedDisconnectionWarningPacketId -> BackendError.TerrainGenerationOutOfSync

            GameConfig config = ConfigManager.Get<GameConfig>();

            GameObject player = new();

            GameObjectSettings settings = new()
            {
                Template = HashUtility.MurmurHash2(config.CharacterTemplate),
                Id = 0x1,
                GridPosition = _startOverride != null ? _startOverride.Value : new(config.StartPositionX, config.StartPositionY),
            };

            // component order is strict for serialization
            player.Initialize(ref settings);    // Positioned instantiated in Initialize()
            player.GetOrCreateComponent<LifeComponent>().Life = 100;
            player.GetOrCreateComponent<AnimatedComponent>();

            PlayerComponent playerComponent = player.GetOrCreateComponent<PlayerComponent>();
            playerComponent.Name = _characterName;
            if (_area == "1_1_1")
            {
                player.GetComponent<PositionedComponent>().Rotation = 3.14f;
                playerComponent.IsWashedUp = true;
            }

            player.GetOrCreateComponent<InventoriesComponent>();
            player.GetOrCreateComponent<ActorComponent>();

            using MemoryStream ms = new();
            player.Serialize(ms);

            var objAdd = PacketFactory.Get<InstanceClientObjectAddPacket>();
            objAdd.Blob = ms.ToArray();
            Send(objAdd);
        }

        #endregion
    }
}
