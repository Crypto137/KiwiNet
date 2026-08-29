namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceUseItemOnItem : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }
        public byte Field2 { get; set; }
        public uint Field3 { get; set; }

        public ClientInstanceUseItemOnItem() : base(PacketId.ClientInstanceUseItemOnItemId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
            Field1 = PacketIO.ReadUInt32(stream);
            Field2 = PacketIO.ReadByte(stream);
            Field3 = PacketIO.ReadUInt32(stream);
        }
    }
}
