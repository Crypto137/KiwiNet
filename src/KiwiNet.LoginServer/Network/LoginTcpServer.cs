using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;

namespace KiwiNet.LoginServer.Network
{
    public sealed class LoginTcpServer : TcpServer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public bool Initialize()
        {
            Run();
            return true;
        }

        public override void Run()
        {
            LoginServerConfig config = ConfigManager.Get<LoginServerConfig>();
            string bindIP = config.BindIP;
            int port = config.Port;

            if (Start(bindIP, port) == false)
                return;

            Logger.Info($"Listening on {bindIP}:{port}...");
        }

        protected override void OnClientConnected(NetworkConnection connection)
        {
            LoginServerApp.Instance.LoginService.OnClientConnected(connection);
        }
    }
}
