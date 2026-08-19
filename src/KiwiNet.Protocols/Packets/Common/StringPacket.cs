namespace KiwiNet.Protocols.Packets.Common
{
    public sealed class StringPacket : Packet
    {
        public string Value { get; set; } = string.Empty;

        public StringPacket(PacketId id) : base(id)
        {
        }

        public override string ToString()
        {
            return Value;
        }

        protected override void DeserializeData(Stream stream)
        {
            Value = PacketIO.ReadStringUtf16(stream);
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteString(stream, Value);
        }
    }
}
