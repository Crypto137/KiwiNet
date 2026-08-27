using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Math;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Commands;
using KiwiNet.InstanceServer.GameData;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Components;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public class RemotePlayer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly InstanceTcpClient _client;

        public Area Area { get; }

        public GameObject Player { get; private set; }

        public RemotePlayer(Area area, InstanceTcpClient client)
        {
            Area = area;
            _client = client;
        }

        public void Send(Packet packet)
        {
            _client.Send(packet);
        }

        public void Load()
        {
            GameConfig config = ConfigManager.Get<GameConfig>();

            // TODO: create Player game object via GameObjectManager, load persistent data here
            Player = new();

            GameObjectSettings settings = new()
            {
                Template = HashUtility.MurmurHash2(config.CharacterTemplate),
                Id = 0x1,
                GridPosition = _client.Session.StartPosition,
            };

            // component order is strict for serialization
            Player.Initialize(ref settings);    // Positioned instantiated in Initialize()
            Player.GetOrCreateComponent<LifeComponent>().Life = 100;
            Player.GetOrCreateComponent<AnimatedComponent>();

            PlayerComponent playerComponent = Player.GetOrCreateComponent<PlayerComponent>();
            playerComponent.Name = _client.Session.CharacterName;
            if (Area.WorldAreaId == "1_1_1")
            {
                Player.GetComponent<PositionedComponent>().Rotation = 3.14f;
                //playerComponent.IsWashedUp = true;
            }

            Player.GetOrCreateComponent<InventoriesComponent>();
            Player.GetOrCreateComponent<ActorComponent>();

            //---

            InstanceClientInstanceInformationPacket instanceInfo = PacketFactory.Get<InstanceClientInstanceInformationPacket>();
            instanceInfo.PlayerObjectId = Player.Id;
            instanceInfo.WorldAreaId = Area.WorldAreaId;
            instanceInfo.League = Area.League;
            instanceInfo.Seed = Area.Seed;
            Send(instanceInfo);
        }

        public bool BeginAreaTransfer(string areaId, Vector2Int startOverride = default)
        {
            if (WorldAreaTable.IsValidAreaId(areaId) == false)
                return false;

            ClientSession session = _client.Session;

            session.WorldAreaId = areaId;
            session.StartPosition = startOverride;

            StringPacket notification = PacketFactory.Get<StringPacket>(PacketId.InstanceClientAreaChangeNotificationPacketId);
            notification.Value = areaId;
            Send(notification);

            InstanceClientInstanceDetailsPacket instanceDetails = PacketFactory.Get<InstanceClientInstanceDetailsPacket>();
            instanceDetails.SessionId = session.Id;
            instanceDetails.Field1 = 0;
            instanceDetails.WorldAreaId = areaId;
            instanceDetails.Entries.Add(new("localhost", "6112"));
            Send(instanceDetails);
            return true;
        }

        #region Message Handling

        public void ReceivePacket(Packet packet)
        {
            switch (packet.Id)
            {
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
            reply.Name = Player.GetComponent<PlayerComponent>().Name;
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

            using MemoryStream ms = new();
            Player.Serialize(ms);

            var objAdd = PacketFactory.Get<InstanceClientObjectAddPacket>();
            objAdd.Blob = ms.ToArray();
            Send(objAdd);
        }

        #endregion
    }
}
