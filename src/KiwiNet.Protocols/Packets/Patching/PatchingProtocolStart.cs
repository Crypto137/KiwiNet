namespace KiwiNet.Protocols.Packets.Patching
{
    public sealed class PatchingProtocolStart : Packet
    {
        public byte[] Field0 { get; } = new byte[32];
        public string Field1 { get; }

        public PatchingProtocolStart() : base(PacketId.PatchingProtocolStartId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(Field0);
            PacketIO.WriteString(stream, Field1);
        }
    }
}
