namespace KiwiNet.Protocols.Packets.Common
{
    public sealed class BackendErrorPacket : Packet
    {
        public BackendError Value { get; set; }

        public BackendErrorPacket(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Value = (BackendError)PacketIO.ReadByte(stream);
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteByte(stream, (byte)Value);
        }
    }
}
