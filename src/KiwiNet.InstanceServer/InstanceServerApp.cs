using KiwiNet.Core.System;
using KiwiNet.InstanceServer.Network;

namespace KiwiNet.InstanceServer
{
    public sealed class InstanceServerApp : ServerApp
    {
        public InstanceTcpServer TcpServer { get; } = new();

        public static InstanceServerApp Instance { get; } = new();

        private InstanceServerApp() : base("InstanceServer", "KiwiNet.InstanceServer.Config")
        {
        }

        protected override bool InitializeSystems()
        {
            return TcpServer.Initialize();
        }

        protected override void DisposeSystems()
        {
        }

        protected override void HandleInput(string input)
        {
        }
    }
}
