namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct PartyChangedEntry(string field0, byte field1)
    {
        public readonly string Field0 = field0;
        public readonly byte Field1 = field1;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Field0);
            PacketIO.WriteByte(stream, Field1);
        }
    }

    public sealed class InstanceClientPartyChanged : Packet
    {
        public uint Field0 { get; }
        public List<PartyChangedEntry> Entries { get; } = new();

        public InstanceClientPartyChanged() : base(PacketId.InstanceClientPartyChangedId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);

            PacketIO.WriteByte(stream, (byte)Entries.Count);
            foreach (PartyChangedEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
