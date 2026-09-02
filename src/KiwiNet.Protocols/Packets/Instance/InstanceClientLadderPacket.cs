using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct LadderEntry(string field0, uint field1, uint field2)
    {
        public readonly string Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly uint Field2 = field2;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
            connection.Write(Field2);
        }
    }

    public sealed class InstanceClientLadderPacket : Packet
    {
        public List<LadderEntry> Entries { get; } = new();

        public InstanceClientLadderPacket() : base(PacketId.InstanceClientLadderPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Entries.Count);
            foreach (LadderEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
