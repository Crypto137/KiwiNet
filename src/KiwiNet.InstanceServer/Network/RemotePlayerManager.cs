using KiwiNet.Core.Collections;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Areas;

namespace KiwiNet.InstanceServer.Network
{
    public class RemotePlayerManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<NetworkConnection, RemotePlayer> _players = new();

        private readonly DoubleBufferQueue<NetworkConnection> _addClientQueue = new();
        private readonly DoubleBufferQueue<NetworkConnection> _removeClientQueue = new();

        public Area Area { get; }

        public int PlayerCount { get => _players.Count; }

        public RemotePlayerManager(Area worldArea)
        {
            Area = worldArea;
        }

        public Dictionary<NetworkConnection, RemotePlayer>.ValueCollection.Enumerator GetEnumerator()
        {
            return _players.Values.GetEnumerator();
        }

        public void AddPlayer(NetworkConnection connection)
        {
            _addClientQueue.Enqueue(connection);
        }

        public void RemovePlayer(NetworkConnection connection)
        {
            _removeClientQueue.Enqueue(connection);
        }

        public void Update()
        {
            _removeClientQueue.Swap();
            while (_removeClientQueue.CurrentCount > 0)
            {
                NetworkConnection connection = _removeClientQueue.Dequeue();
                _players.Remove(connection, out RemotePlayer player);
                ClientSession session = player.Session;
                if (session.Connection == connection)
                    InstanceServerApp.Instance.ClientSessionManager.RemoveSession(session.Id);
                Logger.Debug($"Removed player from area [{Area}]");
            }

            _addClientQueue.Swap();
            while (_addClientQueue.CurrentCount > 0)
            {
                NetworkConnection connection = _addClientQueue.Dequeue();
                ClientSession session = InstanceServerApp.Instance.ClientSessionManager.GetSessionForConnection(connection); // FIXME
                RemotePlayer player = new(Area, connection, session);
                _players.Add(connection, player);
                Logger.Debug($"Added player to area [{Area}]");

                player.Load();
            }

            foreach (RemotePlayer player in _players.Values)
                player.Receive();
        }
    }
}
