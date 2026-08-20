namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceChatMessagePacket : Packet
    {
        public string Text { get; set; } = string.Empty;
        public List<object> Items { get; } = new();

        public ClientInstanceChatMessagePacket() : base(PacketId.ClientInstanceChatMessagePacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Text = PacketIO.ReadString(stream);

            int numItems = PacketIO.ReadByte(stream);
            for (int i = 0; i < numItems; i++)
            {
                // TODO: polymorphic data for linked items
                return;
            }
        }
    }
}
