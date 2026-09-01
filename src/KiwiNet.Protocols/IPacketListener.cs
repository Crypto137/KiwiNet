using KiwiNet.Core.Network;

namespace KiwiNet.Protocols
{
    public interface IPacketListener
    {
        public bool ReceivePacket(PacketId packetId, BufferedNetworkStream stream);
    }
}
