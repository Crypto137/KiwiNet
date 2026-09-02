using System.Net;
using System.Net.Sockets;
using KiwiNet.Core.Logging;

namespace KiwiNet.Core.Network
{
    /// <summary>
    /// An abstract TCP server implementation.
    /// </summary>
    public abstract class TcpServer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private CancellationTokenSource _cts;

        private Socket _listener;
        private bool _isListening;

        protected bool _isRunning;

        /// <summary>
        /// Runs the server. This method should generally be executed by its own <see cref="Thread"/>.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Creates a new socket and begins listening on the specified IP and port.
        /// </summary>
        public virtual bool Start(string bindIP, int port)
        {
            if (_isListening) throw new InvalidOperationException("Server is already listening.");

            // Reset CTS
            _cts?.Dispose();
            _cts = new();

            // Create a new listener socket
            _listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = true,
                LingerState = new(false, 0)
            };

            // Try to bind it
            try
            {
                _listener.Bind(new IPEndPoint(IPAddress.Parse(bindIP), port));
            }
            catch (SocketException)
            {
                Logger.Fatal($"{GetType().Name} cannot bind on {bindIP}, server shutting down...");
                Shutdown();
                return false;
            }

            // Start listening
            _listener.Listen();
            _isListening = true;

            // Start accepting connections
            _ = Task.Run(AcceptConnectionsAsync);

            _isRunning = true;

            return true;
        }

        /// <summary>
        /// Cancels async tasks, stops listening for connections, and disconnects all connected clients.
        /// </summary>
        public virtual void Shutdown()
        {
            if (_isListening == false) return;

            // Cancel async tasks
            _cts.Cancel();

            // Close the listener socket
            _listener?.Close();
            _listener = null;
            _isListening = false;

            _isRunning = false;
        }

        #region Events

        /// <summary>
        /// Raised when a client connects.
        /// </summary>
        protected abstract void OnClientConnected(NetworkConnection connection);

        #endregion

        private void AddClientConnection(Socket socket)
        {
            NetworkConnection connection = new(socket);
            OnClientConnected(connection);
        }

        /// <summary>
        /// Accepts incoming client connections asynchronously.
        /// </summary>
        private async Task AcceptConnectionsAsync()
        {
            while (_cts.IsCancellationRequested == false)
            {
                try
                {
                    // Wait for a connection
                    Socket socket = await _listener.AcceptAsync(_cts.Token);

                    // Establish a new client connection
                    AddClientConnection(socket);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                    break;
                }
            }
        }
    }
}
