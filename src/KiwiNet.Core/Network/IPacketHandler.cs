namespace KiwiNet.Core.Network
{
    /// <summary>
    /// Processes incoming <see cref="Packet"/> instances deserialized by <see cref="PacketSerializer"/>.
    /// </summary>
    public interface IPacketHandler
    {
        public void HandlePacket(NetworkConnection connection, Packet packet);
    }
}
