namespace KiwiNet.Protocols.Packets.Patching
{
    public readonly struct PatchingFolderEntry
    {
        public readonly byte Field0;
        public readonly string Field1;
        public readonly uint Field2;
        public readonly byte[] Field3;  // 32 bytes

        public void Serialize(Stream stream)
        {
            PacketIO.WriteByte(stream, Field0);
            PacketIO.WriteString(stream, Field1);
            PacketIO.WriteUInt32(stream, Field2);
            stream.Write(Field3);
        }
    }

    public sealed class PatchingFolderContents : Packet
    {
        public string Field0 { get; set; }
        public List<PatchingFolderEntry> Entries { get; } = new();

        public PatchingFolderContents() : base(PacketId.PatchingFolderContentsId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteString(stream, Field0);

            PacketIO.WriteInt32(stream, Entries.Count);
            foreach (PatchingFolderEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
