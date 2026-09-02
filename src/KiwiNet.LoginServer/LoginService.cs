using KiwiNet.Core.Collections;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;
using KiwiNet.LoginServer.Network;
using System.Globalization;

namespace KiwiNet.LoginServer
{
    public class LoginService
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly DoubleBufferQueue<NetworkConnection> _pendingClients = new();
        private readonly Dictionary<NetworkConnection, LoginClient> _clients = new();

        private bool _isRunning;
        private Thread _thread;

        public LoginService() { }

        public bool Initialize()
        {
            if (_isRunning)
                return true;

            _thread = new(Run)
            {
                Name = "LoginService",
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
            Logger.Info("Client connected");
            _pendingClients.Enqueue(connection);
        }

        public void OnClientDisconnected(NetworkConnection connection)
        {
            Logger.Info("Client disconnected");
            _clients.Remove(connection);
        }

        private void Run()
        {
            Logger.Info("LoginService started");

            while (_isRunning)
            {
                Update();
                Thread.Sleep(1);
            }

            Logger.Info("LoginService stopped");
        }

        private void Update()
        {
            _pendingClients.Swap();
            while (_pendingClients.CurrentCount > 0)
            {
                NetworkConnection connection = _pendingClients.Dequeue();
                LoginClient client = new(this, connection);
                _clients.Add(connection, client);
            }

            // Updating clients can cause OnClientDisconnected() to be called, removing the client from the dictionary.
            // This is fine because C# dictionaries support removal during iteration, but we can probably do better.
            foreach (LoginClient client in _clients.Values)
                client.Update();
        }
    }
}
