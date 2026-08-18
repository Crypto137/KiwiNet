namespace KiwiNet.Core.Network.Tcp
{
    /// <summary>
    /// Base class for <see cref="TcpServer"/> clients.
    /// </summary>
    public abstract class TcpClient
    {
        public TcpClientConnection Connection { get; internal set; }

        /// <summary>
        /// Raised when the server receives data from a client connection.
        /// </summary>
        public abstract void OnDataReceived(byte[] buffer, int length);
    }
}
