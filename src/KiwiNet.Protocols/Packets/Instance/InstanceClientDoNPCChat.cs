using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientDoNPCChat : Packet
    {
        public uint Field0 { get; set; }
        public byte Field1 { get; set; }

        public InstanceClientDoNPCChat(PacketId id) : base(id)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
        }
    }
}
