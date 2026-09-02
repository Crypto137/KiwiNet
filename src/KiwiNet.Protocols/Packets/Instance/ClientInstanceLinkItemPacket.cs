using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLinkItemPacket : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public uint Field2 { get; set; }

        public ClientInstanceLinkItemPacket() : base(PacketId.ClientInstanceLinkItemPacketId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Field1 = connection.Read<byte>();
            Field2 = connection.Read<uint>();
        }
    }
}
