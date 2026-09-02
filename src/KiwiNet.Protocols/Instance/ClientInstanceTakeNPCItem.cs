using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceTakeNPCItem : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public uint Field2 { get; set; }
        public uint Field3 { get; set; }
        public byte Field4 { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Field1 = connection.Read<byte>();
            Field2 = connection.Read<uint>();
            Field3 = connection.Read<uint>();
            Field4 = connection.Read<byte>();
        }
    }
}
