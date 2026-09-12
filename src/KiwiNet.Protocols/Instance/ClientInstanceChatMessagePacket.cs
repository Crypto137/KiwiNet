using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceChatMessagePacket : Packet
    {
        public string Text { get; set; } = string.Empty;
        public List<int> ItemLinks { get; } = new();

        public override void Deserialize(NetworkConnection connection)
        {
            Text = connection.ReadString();

            byte numLinks = connection.Read<byte>();
            for (int i = 0; i < numLinks; i++)
            {
                int linkIndex = connection.Read<int>();
                ItemLinks.Add(linkIndex);
            }
        }
    }
}
