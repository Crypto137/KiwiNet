using KiwiNet.Core.Extensions;
using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientChatMessagePacket : Packet
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<(int, ReadOnlyMemory<byte>)> Items { get; } = new();

        public InstanceClientChatMessagePacket() : base(PacketId.InstanceClientChatMessagePacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write(Text);
            // Error message: Tried to serialise more than 255 items in one packet
            connection.Write((byte)Items.Count);
            foreach ((int index, ReadOnlyMemory<byte> blob) in Items)
            {
                connection.Write(index);
                connection.Write(blob.Span);
            }
        }
    }
}
