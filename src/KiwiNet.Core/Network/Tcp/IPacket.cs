namespace KiwiNet.Core.Network.Tcp
{
    /// <summary>
    /// Exposes a packet's serialization routine.
    /// </summary>
    public interface IPacket : IDisposable
    {
        /// <summary>
        /// The minimum buffer size needed to serialize this <see cref="IPacket"/>.
        /// </summary>
        public int SerializedSize { get; }

        /// <summary>
        /// Serializes this <see cref="IPacket"/> to the provided buffer at the specified offset.
        /// </summary>
        /// <returns>The number of bytes written to the buffer.</returns>
        public int Serialize(byte[] buffer, int offset);
    }
}
