using KiwiNet.Core.Network;
using System.Runtime.InteropServices;

namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct InstanceListEntry(ulong field0, uint field1, List<string> field2)
    {
        public readonly ulong Field0 = field0;
        public readonly uint Field1 = field1;
        public readonly List<string> Field2 = field2;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(MemoryMarshal.AsBytes([Field0]));  // 8 bytes, no endianness swap
            connection.Write(Field1);
            connection.Write((byte)Field2.Count);
            foreach (string str in Field2)
                connection.Write(str);
        }
    }

    public sealed class InstanceClientInstanceList : Packet
    {
        public string Field0 { get; set; } = string.Empty;
        public List<InstanceListEntry> Entries { get; } = new();

        public InstanceClientInstanceList() : base(PacketId.InstanceClientInstanceListId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write((byte)Entries.Count);
            foreach (InstanceListEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
