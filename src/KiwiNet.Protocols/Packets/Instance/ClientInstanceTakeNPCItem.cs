namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceTakeNPCItem : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public uint Field2 { get; set; }
        public uint Field3 { get; set; }
        public byte Field4 { get; set; }

        public ClientInstanceTakeNPCItem(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadUInt32(stream);
            Field1 = PacketIO.ReadByte(stream);
            Field2 = PacketIO.ReadUInt32(stream);
            Field3 = PacketIO.ReadUInt32(stream);
            Field4 = PacketIO.ReadByte(stream);
        }
    }
}
