using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceUseItemOnItem : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }
        public byte Field2 { get; set; }
        public uint Field3 { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
            Field1 = connection.Read<uint>();
            Field2 = connection.Read<byte>();
            Field3 = connection.Read<uint>();
        }
    }
}
