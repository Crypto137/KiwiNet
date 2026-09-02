using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLiftItem : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }

        public ClientInstanceLiftItem() : base(PacketId.ClientInstanceLiftItemId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Field1 = connection.Read<byte>();
        }
    }
}
