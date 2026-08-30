namespace KiwiNet.Protocols.Packets.Patching
{
    public sealed class PatchingProtocolVersion : Packet
    {
        public byte Field0 { get; set; }

        public PatchingProtocolVersion() : base(PacketId.PatchingProtocolVersionId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
        }
    }
}
