namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectAddPacket : Packet
    {
        public Memory<byte> Blob { get; set; } = Array.Empty<byte>();

        public InstanceClientObjectAddPacket() : base((PacketId)100)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(Blob.Span);
        }
    }
}
