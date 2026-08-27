using KiwiNet.Core.System;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Network;

namespace KiwiNet.InstanceServer
{
    public sealed class InstanceServerApp : ServerApp
    {
        public AreaManager AreaManager { get; } = new();
        public ClientSessionManager ClientSessionManager { get; } = new();
        public InstanceTcpServer TcpServer { get; } = new();

        public static InstanceServerApp Instance { get; } = new();

        private InstanceServerApp() : base("InstanceServer", "KiwiNet.InstanceServer.Config")
        {
        }

        protected override bool InitializeSystems()
        {
            AreaManager.Initialize();
            return TcpServer.Initialize();
        }

        protected override void DisposeSystems()
        {
            TcpServer.Shutdown();
            AreaManager.Shutdown();
        }

        protected override void HandleInput(string input)
        {
        }
    }
}
