using KiwiNet.Core.Extensions;
using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientChatMessagePacket : Packet
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<(int, ItemData)> Items { get; } = new();

        public InstanceClientChatMessagePacket() : base(PacketId.InstanceClientChatMessagePacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteString(stream, Name);
            PacketIO.WriteString(stream, Text);

            // Error message: Tried to serialise more than 255 items in one packet
            stream.Write((byte)Items.Count);
            foreach ((int index, ItemData item) in Items)
            {
                PacketIO.WriteInt32(stream, index);
                item.Serialize(stream);
            }
        }
    }
}
