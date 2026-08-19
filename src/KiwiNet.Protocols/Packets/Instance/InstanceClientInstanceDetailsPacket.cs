using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientInstanceDetailsPacket : Packet
    {
        public uint Field0 { get; set; }
        public uint Field1 { get; set; }
        public string Field2 { get; set; } = string.Empty;
        public List<InstanceDetailsEntry> Entries { get; } = new();

        public InstanceClientInstanceDetailsPacket() : base(PacketId.InstanceClientInstanceDetailsPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteUInt32(stream, Field1);
            PacketIO.WriteString(stream, Field2);

            PacketIO.WriteUInt8(stream, (byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
