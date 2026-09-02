using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientContactUpdate : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }
        public string Field2 { get; set; } 
        public uint Field3 { get; set; }
        public byte Field4 { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
            connection.Write(Field2);
            connection.Write(Field3);
            connection.Write(Field4);
        }
    }
}
