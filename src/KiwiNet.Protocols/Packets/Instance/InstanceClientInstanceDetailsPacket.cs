using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientInstanceDetailsPacket : Packet
    {
        public uint SessionId { get; set; }
        public uint Field1 { get; set; }
        public string WorldAreaId { get; set; } = string.Empty;
        public List<InstanceDetailsEntry> Entries { get; } = new();

        public InstanceClientInstanceDetailsPacket() : base(PacketId.InstanceClientInstanceDetailsPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, SessionId);
            PacketIO.WriteUInt32(stream, Field1);
            PacketIO.WriteString(stream, WorldAreaId);

            PacketIO.WriteByte(stream, (byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
