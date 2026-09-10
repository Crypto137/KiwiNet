using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Math;
using KiwiNet.Core.Network;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Commands;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.Resources.Tables;
using KiwiNet.InstanceServer.WorldObjects;
using KiwiNet.InstanceServer.WorldObjects.Components;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Common;
using KiwiNet.Protocols.Instance;
using System.Diagnostics;

namespace KiwiNet.InstanceServer.Network
{
    public class RemotePlayer : IPacketHandler
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly List<PacketSerializer> _packetSerializers;

        public Area Area { get; }
        public NetworkConnection Connection { get; }
        public ClientSession Session { get; }

        public WorldObject Player { get; private set; }

        public RemotePlayer(Area area, NetworkConnection connection, ClientSession session)
        {
            _packetSerializers = new() { new ClientGamePacketSerializer(this) };

            Area = area;
            Connection = connection;
            Session = session;
        }

        public void Receive()
        {
            Connection.Receive();
            PacketSerializer.DeserializeAllPackets(Connection, _packetSerializers);
        }

        public void Send(Packet packet)
        {
            Connection.Write(packet.Id);
            packet.Serialize(Connection);
            Connection.Flush();
        }

        public void SendWorldObjectAdd(WorldObject worldObject)
        {
            Connection.Write((byte)WorldObjectPacketId.InstanceClientWorldObjectAdd);
            worldObject.Serialize(Connection);
            Connection.Flush();
        }

        public void Load()
        {
            GameConfig config = ConfigManager.Get<GameConfig>();

            // TODO: create the Player object via WorldObjectManager, load persistent data here
            Player = new() { Id = 0x1 };

            using ResourceHandle<WorldObjectRegistry> worldObjectTable = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            
            ResourceHandle<WorldObjectTemplate> playerTemplate = worldObjectTable.Resource.GetTemplate(config.CharacterTemplate);
            if (playerTemplate == null)
            {
                Logger.Warn($"{config.CharacterTemplate} is not a valid character template, falling back to Str");
                playerTemplate = worldObjectTable.Resource.GetTemplate("Str");
            }

            Player.Initialize(playerTemplate);

            Player.Positioned.SetPosition(Session.StartPosition);
            Player.GetComponent<LifeComponent>().Life = 100;

            PlayerComponent playerComponent = Player.GetComponent<PlayerComponent>();
            playerComponent.Name = Session.CharacterName;
            if (Area.WorldAreaId == "1_1_1")
            {
                Player.GetComponent<PositionedComponent>().Rotation = 3.14f;
                //playerComponent.IsWashedUp = true;
            }

            playerTemplate.DecrementRefCount();

            //---

            InstanceClientInstanceInformationPacket instanceInfo = PacketFactory.Get<InstanceClientInstanceInformationPacket>();
            instanceInfo.Id = (byte)PacketId.InstanceClientInstanceInformationPacketId;
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

            Session.WorldAreaId = areaId;
            Session.StartPosition = startOverride;

            StringPacket notification = PacketFactory.Get<StringPacket>();
            notification.Id = (byte)PacketId.InstanceClientAreaChangeNotificationPacketId;
            notification.Value = areaId;
            Send(notification);

            InstanceClientInstanceDetailsPacket instanceDetails = PacketFactory.Get<InstanceClientInstanceDetailsPacket>();
            instanceDetails.Id = (byte)PacketId.InstanceClientInstanceDetailsPacketId;
            instanceDetails.SessionId = Session.Id;
            instanceDetails.Field1 = 0;
            instanceDetails.WorldAreaId = areaId;
            instanceDetails.Entries.Add(new("localhost", "6112"));
            Send(instanceDetails);
            return true;
        }

        #region Message Handling

        public void HandlePacket(NetworkConnection connection, Packet packet)
        {
            Debug.Assert(connection == Connection);

            if (packet == null)
            {
                Connection.Disconnect();
                Area.RemotePlayerManager.RemovePlayer(Connection);
                return;
            }

            switch ((PacketId)packet.Id)
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
                    Logger.Warn($"Unhandled packet [{(PacketId)packet.Id}] {packet.Id}");
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
            reply.Id = (byte)PacketId.InstanceClientChatMessagePacketId;
            reply.Name = Player.GetComponent<PlayerComponent>().Name;
            reply.Text = chatMessage.Text;
            Send(reply);
        }

        private void OnHeartbeat()
        {
            Send(PacketFactory.Get<SimplePacket>((byte)PacketId.InstanceClientHeartbeatReplyPacketId));
#if DEBUG
            InstanceClientServerFrameDurationPacket serverFrameDuration = PacketFactory.Get<InstanceClientServerFrameDurationPacket>();
            serverFrameDuration.Id = (byte)PacketId.InstanceClientServerFrameDurationPacketId;
            serverFrameDuration.ServerFrameTimeMS = (short)Area.LastFrameTime.TotalMilliseconds;
            Send(serverFrameDuration);
#endif
        }

        private void OnSkillTargetEntity(Packet packet)
        {
            Logger.Debug($"OnSkillTargetEntity(): {packet}");
        }

        private void OnSkillTargetLocation(Packet packet)
        {
            ClientInstanceSkillTargetLocation skillTargetLocation = (ClientInstanceSkillTargetLocation)packet;

            Logger.Debug($"OnSkillTargetLocation(): {skillTargetLocation}");

            PositionedComponent playerPosition = Player.GetComponent<PositionedComponent>();
            playerPosition.SetPosition(new((int)skillTargetLocation.GridPositionX, (int)skillTargetLocation.GridPositionY));
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

            // TODO: some kind of area of interest system
            SendWorldObjectAdd(Player);

            var skills = PacketFactory.Get<InstanceClientBoundSkillList>();
            skills.Id = (byte)PacketId.InstanceClientBoundSkillListId;
            skills.MouseSkills[0] = 0x7D5F79C7;
            Send(skills);
        }

        #endregion
    }
}
