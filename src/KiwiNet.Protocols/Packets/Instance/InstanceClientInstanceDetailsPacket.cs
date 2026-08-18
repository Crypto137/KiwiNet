using KiwiNet.Core.Extensions;
using KiwiNet.Protocols.Packets.Common;
using System.Buffers.Binary;

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
            stream.Write(BinaryPrimitives.ReverseEndianness(Field0));
            stream.Write(BinaryPrimitives.ReverseEndianness(Field1));
            stream.WriteNetworkUtf16String(Field2);

            stream.Write((byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
