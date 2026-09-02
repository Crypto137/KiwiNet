using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceInventoryMovePacket : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }
        public uint Field2 { get; set; }

        public ClientInstanceInventoryMovePacket(PacketId id) : base(id)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
            Field1 = connection.Read<uint>();
            Field2 = connection.Read<uint>();
        }
    }
}
