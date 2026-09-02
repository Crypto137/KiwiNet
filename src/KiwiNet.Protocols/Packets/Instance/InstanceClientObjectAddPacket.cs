using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectAddPacket : Packet
    {
        public Memory<byte> Blob { get; set; } = Array.Empty<byte>();

        public InstanceClientObjectAddPacket() : base((PacketId)100)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Blob.Span);
        }
    }
}
