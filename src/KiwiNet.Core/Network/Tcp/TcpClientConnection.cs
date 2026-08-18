using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using KiwiNet.Core.Logging;

namespace KiwiNet.Core.Network.Tcp
{
    /// <summary>
    /// A wrapper around <see cref="System.Net.Sockets.Socket"/> that represents a TCP server's connection to a client.
    /// </summary>
    public class TcpClientConnection
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly TcpServer _server;
        private readonly byte[] _receiveBuffer;

        private readonly CancellationTokenSource _cts = new();
        private readonly Channel<IPacket> _sendChannel = Channel.CreateUnbounded<IPacket>();

        public Socket Socket { get; }
        public bool Connected { get => Socket.Connected; }
        public IPEndPoint RemoteEndPoint { get => (IPEndPoint)Socket.RemoteEndPoint; }

        public TcpClient Client { get; internal set; }
        public bool IsReceiveTimeoutSuspended { get; set; }

        /// <summary>
        /// Constructs a new client connection instance.
        /// </summary>
        public TcpClientConnection(TcpServer server, Socket socket)
        {
            _server = server;
            Socket = socket;

            _receiveBuffer = new byte[_server.ReceiveBufferSize];   // TODO: reuse receive buffers
            Socket.SendBufferSize = _server.SendBufferSize;
        }

        public override string ToString()
        {
            return RemoteEndPoint != null ? RemoteEndPoint.ToString() : "NULL";
        }

        /// <summary>
        /// Disconnects this client connection.
        /// </summary>
        public void Disconnect()
        {
            if (Connected)
                _server.DisconnectClient(this);
        }

        /// <summary>
        /// Queues an <see cref="IPacket"/> to be sent over this connection.
        /// </summary>
        public void Send<T>(T packet) where T: IPacket
        {
            ArgumentNullException.ThrowIfNull(packet);

            _sendChannel.Writer.TryWrite(packet);
        }

        internal void StartAsyncTasks()
        {
            _ = Task.Run(ReceiveAsync);
            _ = Task.Run(SendAsync);
        }

        internal void StopAsyncTasks()
        {
            _cts.Cancel();
        }

        /// <summary>
        /// Receives data from a <see cref="TcpClientConnection"/> asynchronously.
        /// </summary>
        private async Task ReceiveAsync()
        {
            while (_cts.IsCancellationRequested == false)
            {
                try
                {
                    Task<int> receiveTask = Socket.ReceiveAsync(_receiveBuffer.AsMemory(), _cts.Token).AsTask();

                    if (_server.ReceiveTimeoutMS > 0)
                    {
                        await receiveTask.WaitAsync(TimeSpan.FromMilliseconds(_server.ReceiveTimeoutMS));

                        if (_cts.Token.IsCancellationRequested)
                            break;

                        if (IsReceiveTimeoutSuspended == false && receiveTask.IsCompleted == false)
                        {
                            Logger.Warn($"ReceiveDataAsync(): Connection to {this} timed out");
                            break;
                        }
                    }

                    int bytesReceived = await receiveTask;

                    if (bytesReceived == 0)             // Connection lost
                        break;

                    IsReceiveTimeoutSuspended = false;

                    // Do the OnDataReceived() callback to parse received data from the connection's buffer.
                    Client.OnDataReceived(_receiveBuffer, bytesReceived);

                    if (Connected == false)  // Stop receiving if no longer connected
                        break;
                }
                catch (SocketException)
                {
                    break;
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

            _server.DisconnectClient(this);
        }

        private async Task SendAsync()
        {
            while (_cts.IsCancellationRequested == false)
            {
                try
                {
                    IPacket packet = await _sendChannel.Reader.ReadAsync(_cts.Token);
                    await SendPacketAsync(packet);
                }
                catch (SocketException)
                {
                    break;
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

            _server.DisconnectClient(this);
        }

        /// <summary>
        /// Sends an <see cref="IPacket"/> over the provided <see cref="TcpClientConnection"/> asynchronously.
        /// Returns the number of bytes sent.
        /// </summary>
        private async ValueTask<int> SendPacketAsync<T>(T packet, SocketFlags flags = SocketFlags.None) where T : IPacket
        {
            int totalSent = 0;

            byte[] buffer = _server.BufferPool.Rent(packet.SerializedSize);

            try
            {
                int bytesRemaining = packet.Serialize(buffer, 0);

                while (bytesRemaining > 0)
                {
                    ReadOnlyMemory<byte> bytes = buffer.AsMemory(totalSent, bytesRemaining);
                    int sent = await Socket.SendAsync(bytes, flags, _cts.Token);
                    bytesRemaining -= sent;
                    totalSent += sent;
                }
            }
            finally
            {
                _server.BufferPool.Return(buffer);
                packet.Dispose();
            }

            return totalSent;
        }
    }
}
