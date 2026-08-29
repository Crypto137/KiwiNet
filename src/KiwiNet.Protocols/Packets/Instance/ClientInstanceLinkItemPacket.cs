namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLinkItemPacket : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public uint Field2 { get; set; }

        public ClientInstanceLinkItemPacket() : base(PacketId.ClientInstanceLinkItemPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadUInt32(stream);
            Field1 = PacketIO.ReadByte(stream);
            Field2 = PacketIO.ReadUInt32(stream);
        }
    }
}
