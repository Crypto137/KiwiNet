namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceUseItem : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }

        public ClientInstanceUseItem() : base(PacketId.ClientInstanceUseItemId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
            Field1 = PacketIO.ReadUInt32(stream);
        }
    }
}
