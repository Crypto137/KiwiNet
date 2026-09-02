using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientInstanceDetailsPacket : Packet
    {
        public uint SessionId { get; set; }
        public uint Field1 { get; set; }
        public string WorldAreaId { get; set; } = string.Empty;
        public List<InstanceDetailsEntry> Entries { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(SessionId);
            connection.Write(Field1);
            connection.Write(WorldAreaId);
            connection.Write((byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
