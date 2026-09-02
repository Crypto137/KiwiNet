using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Patching
{
    public readonly struct PatchingFolderEntry
    {
        public readonly byte Field0;
        public readonly string Field1;
        public readonly uint Field2;
        public readonly byte[] Field3;  // 32 bytes

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
            connection.Write(Field2);
            connection.Write(Field3);
        }
    }

    public sealed class PatchingFolderContents : Packet
    {
        public string Field0 { get; set; }
        public List<PatchingFolderEntry> Entries { get; } = new();

        public PatchingFolderContents() : base(PacketId.PatchingFolderContentsId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Entries.Count);
            foreach (PatchingFolderEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
