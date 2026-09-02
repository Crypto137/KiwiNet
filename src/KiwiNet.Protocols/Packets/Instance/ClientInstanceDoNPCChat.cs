using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceDoNPCChat : Packet
    {
        public byte Field0 { get; set; }

        public ClientInstanceDoNPCChat() : base(PacketId.ClientInstanceDoNPCChatId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
        }
    }
}
