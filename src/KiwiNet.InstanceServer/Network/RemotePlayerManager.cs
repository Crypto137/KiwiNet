using KiwiNet.Core.Collections;
using KiwiNet.Core.Logging;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.Network
{
    public class RemotePlayerManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<InstanceTcpClient, RemotePlayer> _players = new();

        private readonly DoubleBufferQueue<InstanceTcpClient> _addClientQueue = new();
        private readonly DoubleBufferQueue<InstanceTcpClient> _removeClientQueue = new();
        private readonly DoubleBufferQueue<(InstanceTcpClient, Packet)> _packetQueue = new();

        public Area Area { get; }

        public int PlayerCount { get => _players.Count; }

        public RemotePlayerManager(Area worldArea)
        {
            Area = worldArea;
        }

        public void AddPlayer(InstanceTcpClient client)
        {
            _addClientQueue.Enqueue(client);
        }

        public void RemovePlayer(InstanceTcpClient client)
        {
            _removeClientQueue.Enqueue(client);
        }

        public void ReceivePacket(InstanceTcpClient client, Packet packet)
        {
            _packetQueue.Enqueue((client, packet));
        }

        public void Update()
        {
            _removeClientQueue.Swap();
            while (_removeClientQueue.CurrentCount > 0)
            {
                InstanceTcpClient removedClient = _removeClientQueue.Dequeue();
                _players.Remove(removedClient);
            }

            _addClientQueue.Swap();
            while (_addClientQueue.CurrentCount > 0)
            {
                InstanceTcpClient addedClient = _addClientQueue.Dequeue();
                RemotePlayer player = new(Area, addedClient);
                _players.Add(addedClient, player);

                player.Load();
            }

            _packetQueue.Swap();
            while (_packetQueue.CurrentCount > 0)
            {
                (InstanceTcpClient client, Packet packet) = _packetQueue.Dequeue();

                if (_players.TryGetValue(client, out RemotePlayer player) == false)
                {
                    Logger.Warn($"No player to receive packet!");
                    continue;
                }

                player.ReceivePacket(packet);
            }
        }
    }
}
