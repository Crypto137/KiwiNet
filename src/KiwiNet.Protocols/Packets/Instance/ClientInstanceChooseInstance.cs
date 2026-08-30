namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceChooseInstance : Packet
    {
        public byte Field0 { get; set; }

        public ClientInstanceChooseInstance() : base(PacketId.ClientInstanceChooseInstanceId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
        }
    }
}
