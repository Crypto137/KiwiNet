using KiwiNet.Core.System;
using KiwiNet.Core.Threading;
using KiwiNet.LoginServer.Accounts;
using KiwiNet.LoginServer.Jobs;
using KiwiNet.LoginServer.Leagues;
using KiwiNet.LoginServer.Network;

namespace KiwiNet.LoginServer
{
    public sealed class LoginServerApp : ServerApp
    {
        // Accounts should obviously be in a separate backend service, but it's easier to have them in login for now.
        // LeagueManager can probably stay as a local non-authoritative cache in the future.
        public AccountManager AccountManager { get; } = new();
        public LeagueManager LeagueManager { get; } = new();
        public JobQueue<LoginJob> JobQueue { get; } = new();
        public LoginTcpServer TcpServer { get; } = new();

        public static LoginServerApp Instance { get; } = new();

        private LoginServerApp() : base("LoginServer", "KiwiNet.LoginServer.Config")
        {
        }

        protected override bool InitializeSystems()
        {
            return AccountManager.Initialize() &&
                   LeagueManager.Initialize() &&
                   JobQueue.Start() &&
                   TcpServer.Initialize();
        }

        protected override void DisposeSystems()
        {
            JobQueue.Stop();
            TcpServer.Shutdown();
        }

        protected override void HandleInput(string input)
        {
        }
    }
}
