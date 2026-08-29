namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceInventoryMovePacket : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }
        public uint Field2 { get; set; }

        public ClientInstanceInventoryMovePacket(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
            Field1 = PacketIO.ReadUInt32(stream);
            Field2 = PacketIO.ReadUInt32(stream);
        }
    }
}
