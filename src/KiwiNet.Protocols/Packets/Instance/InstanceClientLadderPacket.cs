namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct LadderEntry(string field0, uint field1, uint field2)
    {
        public readonly string Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly uint Field2 = field2;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Field0);
            PacketIO.WriteUInt32(stream, Field1);
            PacketIO.WriteUInt32(stream, Field2);
        }
    }

    public sealed class InstanceClientLadderPacket : Packet
    {
        public List<LadderEntry> Entries { get; } = new();

        public InstanceClientLadderPacket() : base(PacketId.InstanceClientLadderPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteInt32(stream, Entries.Count);
            foreach (LadderEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
