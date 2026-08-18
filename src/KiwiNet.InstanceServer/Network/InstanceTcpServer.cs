using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network.Tcp;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class InstanceTcpServer : TcpServer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<TcpClientConnection, InstanceClient> _clients = new();

        public bool Initialize()
        {
            Run();
            return true;
        }

        public override void Run()
        {
            InstanceServerConfig config = ConfigManager.Get<InstanceServerConfig>();
            string bindIP = config.BindIP;
            int port = config.Port;

            ReceiveTimeoutMS = -1;

            if (Start(bindIP, port) == false)
                return;

            Logger.Info($"Listening on {bindIP}:{port}...");
        }

        protected override void OnClientConnected(TcpClientConnection connection)
        {
            Logger.Trace("Client connected");
            _clients[connection] = (InstanceClient)connection.Client;
        }

        protected override void OnClientDisconnected(TcpClientConnection connection)
        {
            Logger.Trace("Client disconnected");
            _clients.Remove(connection);
        }

        protected override TcpClient CreateTcpClient()
        {
            return new InstanceClient();
        }
    }
}
