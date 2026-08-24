namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct InstanceListEntry(ulong field0, uint field1, List<string> field2)
    {
        public readonly ulong Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly List<string> Field2 = field2;

        public void Serialize(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(Field0));    // 8 bytes, no endianness swap?
            PacketIO.WriteUInt32(stream, Field1);

            PacketIO.WriteByte(stream, (byte)Field2.Count);
            foreach (string str in Field2)
                PacketIO.WriteString(stream, str);
        }
    }

    public sealed class InstanceClientInstanceList : Packet
    {
        public string Field0 { get; set; } = string.Empty;
        public List<InstanceListEntry> Entries { get; } = new();

        public InstanceClientInstanceList() : base(PacketId.InstanceClientInstanceListId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteString(stream, Field0);

            PacketIO.WriteByte(stream, (byte)Entries.Count);
            foreach (InstanceListEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
