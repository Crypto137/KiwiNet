using KiwiNet.Core.Extensions;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientChatMessagePacket : Packet
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<object> Items { get; } = new();

        public InstanceClientChatMessagePacket() : base(PacketId.InstanceClientChatMessagePacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteString(stream, Name);
            PacketIO.WriteString(stream, Text);

            // Error message: Tried to serialise more than 255 items in one packet
            stream.Write((byte)Items.Count);
            if (Items.Count > 0)
                throw new NotImplementedException();    // TODO: polymorphic data for linked items
        }
    }
}
