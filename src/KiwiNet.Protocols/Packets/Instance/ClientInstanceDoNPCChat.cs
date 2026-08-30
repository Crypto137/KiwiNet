namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceDoNPCChat : Packet
    {
        public byte Field0 { get; set; }

        public ClientInstanceDoNPCChat() : base(PacketId.ClientInstanceDoNPCChatId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Field0 = PacketIO.ReadByte(stream);
        }
    }
}
