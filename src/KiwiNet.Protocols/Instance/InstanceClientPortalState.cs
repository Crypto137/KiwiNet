using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public readonly struct PortalStateEntry(uint field0, uint field1, string field2)
    {
        public readonly uint Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly string Field2 = field2;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
            connection.Write(Field2);
        }
    }

    public sealed class InstanceClientPortalState : Packet
    {
        public List<PortalStateEntry> Entries { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Entries.Count);
            foreach (PortalStateEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
