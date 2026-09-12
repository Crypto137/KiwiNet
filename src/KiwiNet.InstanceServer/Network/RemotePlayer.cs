using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Math;
using KiwiNet.Core.Network;
using KiwiNet.Core.System;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Commands;
using KiwiNet.InstanceServer.Items;
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
    public enum WorldObjectPacketId
    {
        InstanceClientWorldObjectAdd = 100,
        InstanceClientWorldObjectUpdate,
        InstanceClientWorldObjectRemove,
    }

    public class RemotePlayer : IPacketHandler, IWorldObjectEventSubscriber
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly List<PacketSerializer> _packetSerializers;

        private TimeSpan _lastHeartbeatTime;
        private bool _isDisconnected;

        public Area Area { get; }
        public NetworkConnection Connection { get; }
        public ClientSession Session { get; }

        public WorldObject Player { get; private set; }

        public RemotePlayer(Area area, NetworkConnection connection, ClientSession session)
        {
            _packetSerializers = new() { new ClientGamePacketSerializer(this) };

            _lastHeartbeatTime = Clock.UnixTime;

            Area = area;
            Connection = connection;
            Session = session;
        }

        public void Disconnect()
        {
            if (_isDisconnected)
                return;

            _isDisconnected = true;

            Connection.Disconnect();
            Area.ObjectManager.RemoveSubscriber(this);

            if (Player != null)
            {
                Player.Sleep();
                Player.Destroy();
            }

            Area.RemotePlayerManager.RemovePlayer(Connection);
        }

        public void Receive()
        {
            Connection.Receive();
            PacketSerializer.DeserializeAllPackets(Connection, _packetSerializers);

            if ((Clock.UnixTime - _lastHeartbeatTime) > TimeSpan.FromSeconds(6))
            {
                Logger.Trace("Connection timed out");
                Disconnect();
            }
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

        public void SendWorldObjectUpdate<T>(WorldObject worldObject) where T: WorldComponent
        {
            Connection.Write((byte)WorldObjectPacketId.InstanceClientWorldObjectUpdate);
            worldObject.SerializeUpdate<T>(Connection);
            Connection.Flush();
        }

        public void SendWorldObjectRemove(WorldObject worldObject)
        {
            Connection.Write((byte)WorldObjectPacketId.InstanceClientWorldObjectRemove);
            Connection.Write(worldObject.Id);
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

            Player.Initialize(playerTemplate, Area);
            playerTemplate.DecrementRefCount();

            // hardcoded initialization stuff - fixme
            Player.Positioned.SetPosition(Session.StartPosition);
            Player.GetComponent<Life>().CurrentLife = 100;

            Player playerComponent = Player.GetComponent<Player>();
            playerComponent.Name = Session.CharacterName;
            if (Area.WorldAreaId == "1_1_1")
            {
                Player.GetComponent<Positioned>().Rotation = 3.14f;
                //playerComponent.IsWashedUp = true;
            }

            Inventories inventories = Player.GetComponent<Inventories>();
            Inventory flasks = inventories.GetInventory(InventoryType.Flask1);
            flasks.AddItem(ItemGenerator.Generate("FlaskLife1"), 0, 0);
            flasks.AddItem(ItemGenerator.Generate("FlaskLife1"), 1, 0);
            flasks.AddItem(ItemGenerator.Generate("FlaskMana1"), 4, 0);

            Player.Wake();

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

        #region IWorldObjectEventSubscriber

        public void OnAddObject(WorldObject worldObject)
        {
        }

        public void OnRemoveObject(WorldObject worldObject)
        {
        }

        public void OnWakeObject(WorldObject worldObject)
        {
            // TODO: area of interest
            SendWorldObjectAdd(worldObject);
        }

        public void OnSleepObject(WorldObject worldObject)
        {
            // TODO: area of interest
            SendWorldObjectRemove(worldObject);
        }

        #endregion

        #region Message Handling

        public void HandlePacket(NetworkConnection connection, Packet packet)
        {
            Debug.Assert(connection == Connection);

            if (packet == null)
            {
                Disconnect();
                return;
            }

            switch ((PacketId)packet.Id)
            {
                case PacketId.ClientInstanceChatMessagePacketId:
                    OnChatMessage(packet);
                    break;

                case PacketId.ClientInstanceQuitRequestPacketId:
                    OnQuitRequest();
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
                    Logger.Warn($"Unhandled packet [{packet.Id}] {(PacketId)packet.Id}");
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
            reply.Name = Player.GetComponent<Player>().Name;
            reply.Text = chatMessage.Text;
            Send(reply);
        }

        private void OnQuitRequest()
        {
            Logger.Trace("Received quit request");
            Disconnect();
        }

        private void OnHeartbeat()
        {
            _lastHeartbeatTime = Clock.UnixTime;
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
            ClientInstanceSkillTargetEntity skillTargetEntity = (ClientInstanceSkillTargetEntity)packet;

            Logger.Debug($"OnSkillTargetEntity(): {packet}");

            if (skillTargetEntity.SkillId == 0xC266)
            {
                WorldObject worldObject = Area.ObjectManager.GetObject(skillTargetEntity.TargetId);
                WorldItem worldItem = worldObject?.GetComponent<WorldItem>();
                
                if (worldItem != null)
                {
                    Item item = worldItem.Item;

                    Inventories inventories = Player.GetComponent<Inventories>();
                    Inventory inventory = inventories.GetInventory(InventoryType.MainInventory1);

                    RectInt rect = new(0, 0, 1, 1);

                    if (inventory.IsBlocked(rect, out _) == false)
                    {
                        uint entryId = inventory.AddItem(item, 0, 0);
                        if (entryId != Inventory.InvalidEntryId)
                        {
                            worldItem.Item = null;
                            worldObject.Sleep();
                            worldObject.Destroy();

                            SendWorldObjectUpdate<Inventories>(Player);
                        }
                    }
                }
            }
        }

        private void OnSkillTargetLocation(Packet packet)
        {
            ClientInstanceSkillTargetLocation skillTargetLocation = (ClientInstanceSkillTargetLocation)packet;

            Logger.Debug($"OnSkillTargetLocation(): {skillTargetLocation}");

            Positioned playerPosition = Player.GetComponent<Positioned>();
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
            WorldObjectManager objectManager = Area.ObjectManager;
            foreach (WorldObject worldObject in objectManager)
                SendWorldObjectAdd(worldObject);
            objectManager.AddSubscriber(this);

            var skills = PacketFactory.Get<InstanceClientBoundSkillList>();
            skills.Id = (byte)PacketId.InstanceClientBoundSkillListId;
            skills.MouseSkills[0] = 0x7D5F79C7;
            Send(skills);
        }

        #endregion
    }
}
