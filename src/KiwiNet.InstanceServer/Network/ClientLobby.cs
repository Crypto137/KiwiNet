using KiwiNet.Core.Collections;
using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Instance;
using System.Globalization;

namespace KiwiNet.InstanceServer.Network
{
    /// <summary>
    /// Manages client <see cref="NetworkConnection"/> instances until they authenticate.
    /// </summary>
    public sealed class ClientLobby : IPacketHandler
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly List<PacketSerializer> _packetSerializers;

        private readonly DoubleBufferQueue<NetworkConnection> _pendingClients = new();
        private readonly HashSet<NetworkConnection> _clients = new();

        private bool _isRunning;
        private Thread _thread;

        public ClientLobby()
        {
            _packetSerializers = new() { new ClientLobbyPacketSerializer(this) };
        }

        public bool Initialize()
        {
            if (_isRunning)
                return true;

            _thread = new(Run)
            {
                Name = "ClientLobby",
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
            };

            _thread.Start();

            _isRunning = true;
            return true;
        }

        public void Shutdown()
        {
            _isRunning = false;
            _thread = null;
        }

        public void OnClientConnected(NetworkConnection connection)
        {
            _pendingClients.Enqueue(connection);
        }

        public void HandlePacket(NetworkConnection connection, Packet packet)
        {
            if (packet == null)
            {
                connection.Disconnect();
                return;
            }

            switch (packet.Id)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    OnClientLoginAttempt(connection, packet);
                    break;
            }
        }

        private void OnClientLoginAttempt(NetworkConnection connection, Packet packet)
        {
            if (packet is not ClientInstanceLoginAttemptPacket loginAttempt)
            {
                Logger.Warn("OnLoginAttempt(): Invalid packet");
                return;
            }

            _clients.Remove(connection);
            Logger.Debug($"OnLoginAttempt(): {loginAttempt}");

            GameConfig config = ConfigManager.Get<GameConfig>();

            ClientSession session = InstanceServerApp.Instance.ClientSessionManager.GetSession(loginAttempt.SessionId);
            if (session.Connection == null)
            {
                session.Id = loginAttempt.SessionId;
                session.CharacterName = loginAttempt.CharacterName;
                session.WorldAreaId = config.WorldAreaId;
                session.StartPosition = new(config.StartPositionX, config.StartPositionY);
            }
            else
            {
                NetworkConnection existingConnection = session.Connection;
                session.Connection = null;
                existingConnection.Disconnect();
            }

            session.Connection = connection;

            connection.Write((byte)PacketId.InstanceClientLoginAttemptReplyPacketId);
            InstanceClientLoginAttemptReplyPacket reply = PacketFactory.Get<InstanceClientLoginAttemptReplyPacket>();
            reply.Field0 = 1;
            reply.Field1 = "";
            reply.Serialize(connection);
            connection.Flush();

            AreaSettings areaSettings = new()
            {
                WorldAreaId = session.WorldAreaId,
                League = "Default",
                Seed = (uint)config.WorldAreaSeed,
            };

            Area area = InstanceServerApp.Instance.AreaManager.GetOrCreateArea(ref areaSettings);
            if (area == null)
            {
                Logger.Error($"Failed to get or create area! worldAreaId={areaSettings.WorldAreaId}, seed={areaSettings.Seed}");
                connection.Disconnect();
                return;
            }

            area.RemotePlayerManager.AddPlayer(connection);
        }

        private void Run()
        {
            Logger.Info("ClientLobby started");

            while (_isRunning)
            {
                Update();
                Thread.Sleep(1);
            }

            Logger.Info("ClientLobby stopped");
        }

        private void Update()
        {
            _pendingClients.Swap();
            while (_pendingClients.CurrentCount > 0)
            {
                NetworkConnection connection = _pendingClients.Dequeue();
                _clients.Add(connection);
            }

            foreach (NetworkConnection connection in _clients)
            {
                connection.Receive();
                PacketSerializer.DeserializePacket(connection, _packetSerializers);
            }
        }
    }
}
