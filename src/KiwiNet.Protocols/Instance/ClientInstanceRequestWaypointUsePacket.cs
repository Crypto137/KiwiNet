using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceRequestWaypointUsePacket : Packet
    {
        public uint Field0 { get; set; }
        public uint Field1 { get; set; }
        public byte Field2 { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Field1 = connection.Read<uint>();
            Field2 = connection.Read<byte>();
        }
    }
}
