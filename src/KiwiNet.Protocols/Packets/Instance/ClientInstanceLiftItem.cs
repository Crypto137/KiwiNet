namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLiftItem : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }

        public ClientInstanceLiftItem() : base(PacketId.ClientInstanceLiftItemId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadUInt32(stream);
            Field1 = PacketIO.ReadByte(stream);
        }
    }
}
