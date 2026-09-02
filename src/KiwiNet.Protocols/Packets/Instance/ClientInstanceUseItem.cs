using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceUseItem : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }

        public ClientInstanceUseItem() : base(PacketId.ClientInstanceUseItemId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
            Field1 = connection.Read<uint>();
        }
    }
}
