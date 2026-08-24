
namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct PortalStateEntry(uint field0, uint field1, string field2)
    {
        public readonly uint Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly string Field2 = field2;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteUInt32(stream, Field1);
            PacketIO.WriteString(stream, Field2);
        }
    }

    public sealed class InstanceClientPortalState : Packet
    {
        public List<PortalStateEntry> Entries { get; } = new();

        public InstanceClientPortalState() : base(PacketId.InstanceClientPortalStateId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteByte(stream, (byte)Entries.Count);
            foreach (PortalStateEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
