using System.Buffers;
using System.Net;
using System.Net.Sockets;
using KiwiNet.Core.Logging;

namespace KiwiNet.Core.Network.Tcp
{
    /// <summary>
    /// An abstract TCP server implementation.
    /// </summary>
    public abstract class TcpServer : IDisposable
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<Socket, TcpClientConnection> _connections = new();

        private CancellationTokenSource _cts;

        private Socket _listener;
        private bool _isListening;
        private bool _isDisposed;

        protected bool _isRunning;

        public int ReceiveBufferSize { get; protected set; } = 1024 * 8;    // 8 KB, client input should be relatively small
        public int SendBufferSize { get; protected set; } = 1024 * 64;      // large enough for big loading packets
        public int ReceiveTimeoutMS { get; protected set; } = 30000;

        public int ConnectionCount { get => _connections.Count; }

        internal ArrayPool<byte> BufferPool { get; } = ArrayPool<byte>.Create();

        /// <summary>
        /// Runs the server. This method should generally be executed by its own <see cref="Thread"/>.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Creates a new socket and begins listening on the specified IP and port.
        /// </summary>
        public virtual bool Start(string bindIP, int port)
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name, "Server is disposed.");
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
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name, "Server is disposed.");
            if (_isListening == false) return;

            // Cancel async tasks
            _cts.Cancel();

            // Close the listener socket
            _listener?.Close();
            _listener = null;
            _isListening = false;

            // Disconnect all clients
            DisconnectAllClients();

            _isRunning = false;
        }

        /// <summary>
        /// Disconnects the specified client connection.
        /// </summary>
        internal void DisconnectClient(TcpClientConnection connection)
        {
            // No null check for connection because this is intended to be called only from TcpClientConnection with a this argument.

            Socket socket = connection.Socket;
            if (socket.Connected)
                socket.Disconnect(false);

            RemoveClientConnection(connection.Socket);
        }

        /// <summary>
        /// Disconnects all connected clients.
        /// </summary>
        public void DisconnectAllClients()
        {
            // Disconnect all clients within a single lock to prevent new clients from being added while we do it
            lock (_connections)
            {
                foreach (TcpClientConnection connection in _connections.Values)
                {
                    if (connection.Connected == false)
                        continue;

                    connection.Socket.Disconnect(false);
                    OnClientDisconnected(connection);
                }

                _connections.Clear();
            }
        }

        #region Events

        /// <summary>
        /// Raised when a client connects.
        /// </summary>
        protected abstract void OnClientConnected(TcpClientConnection connection);

        /// <summary>
        /// Raised when a client disconnects.
        /// </summary>
        protected abstract void OnClientDisconnected(TcpClientConnection connection);

        #endregion

        protected abstract TcpClient CreateTcpClient();

        private void AddClientConnection(Socket socket)
        {
            TcpClientConnection connection = new(this, socket);

            lock (_connections)
                _connections.Add(socket, connection);

            // Allocate a TcpClient instance and bind it
            TcpClient client = CreateTcpClient();
            connection.Client = client;
            client.Connection = connection;

            OnClientConnected(connection);
            connection.StartAsyncTasks();
        }

        /// <summary>
        /// Removes the provided <see cref="TcpClientConnection"/> and raises the <see cref="OnClientDisconnected(TcpClientConnection)"/> event.
        /// </summary>
        private void RemoveClientConnection(Socket socket)
        {
            bool removed;
            TcpClientConnection connection;

            lock (_connections)
                removed = _connections.Remove(socket, out connection);

            if (removed && connection != null)
            {
                connection.StopAsyncTasks();
                OnClientDisconnected(connection);
            }
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
                    Socket socket = await _listener.AcceptAsync().WaitAsync(_cts.Token);

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

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            // Dispose of unmanaged resources here.
            if (disposing)
            {
                Shutdown();
                _cts.Dispose();
            }

            _isDisposed = true;
        }

        #endregion
    }
}
