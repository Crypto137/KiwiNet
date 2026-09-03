using KiwiNet.Core.Extensions;
using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientChatMessagePacket : Packet
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<(int, INetworkSerializable)> Items { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write(Text);
            // Error message: Tried to serialise more than 255 items in one packet
            connection.Write((byte)Items.Count);
            foreach ((int index, INetworkSerializable item) in Items)
            {
                connection.Write(index);
                connection.Write(item);
            }
        }
    }
}
