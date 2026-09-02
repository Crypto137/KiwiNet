using KiwiNet.Core.Network;
using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceChatMessagePacket : Packet
    {
        public string Text { get; set; } = string.Empty;
        public List<uint> Items { get; } = new();

        public ClientInstanceChatMessagePacket() : base(PacketId.ClientInstanceChatMessagePacketId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Text = connection.ReadString();

            byte numItems = connection.Read<byte>();
            for (int i = 0; i < numItems; i++)
            {
                uint item = connection.Read<uint>();
                Items.Add(item);
            }
        }
    }
}
