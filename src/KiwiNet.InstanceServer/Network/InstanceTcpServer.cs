using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class InstanceTcpServer : TcpServer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

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

            if (Start(bindIP, port) == false)
                return;

            Logger.Info($"Listening on {bindIP}:{port}...");
        }

        protected override void OnClientConnected(NetworkConnection connection)
        {
            InstanceServerApp.Instance.ClientLobby.OnClientConnected(connection);
        }
    }
}
