namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceStackItemsPacket : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }

        public ClientInstanceStackItemsPacket() : base(PacketId.ClientInstanceUseItemId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
            Field1 = PacketIO.ReadUInt32(stream);
        }
    }
}
