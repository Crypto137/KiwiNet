using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceUseItem : Packet
    {
        public byte Field0 { get; set; }
        public uint Field1 { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
            Field1 = connection.Read<uint>();
        }
    }
}
