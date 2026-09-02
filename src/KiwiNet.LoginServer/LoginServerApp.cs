using KiwiNet.Core.System;
using KiwiNet.LoginServer.Accounts;
using KiwiNet.LoginServer.Leagues;
using KiwiNet.LoginServer.Network;

namespace KiwiNet.LoginServer
{
    public sealed class LoginServerApp : ServerApp
    {
        // Accounts should obviously be in a separate backend service, but it's easier to have them in login for now.
        // LeagueManager can probably stay as a local non-authoritative cache in the future.
        public LoginService LoginService { get; } = new();
        public AccountManager AccountManager { get; } = new();
        public LeagueManager LeagueManager { get; } = new();
        public LoginTcpServer TcpServer { get; } = new();

        public static LoginServerApp Instance { get; } = new();

        private LoginServerApp() : base("LoginServer", "KiwiNet.LoginServer.Config")
        {
        }

        protected override bool InitializeSystems()
        {
            return LoginService.Initialize() &&
                   AccountManager.Initialize() &&
                   LeagueManager.Initialize() &&
                   TcpServer.Initialize();
        }

        protected override void DisposeSystems()
        {
            TcpServer.Shutdown();
            LoginService.Shutdown();
        }

        protected override void HandleInput(string input)
        {
        }
    }
}
