using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientInstanceInformationPacket : Packet
    {
        public uint Field0 { get; set; }                        // probably some kind of runtime instance id?
        public string WorldAreaId { get; set; } = string.Empty; // id column in the WorldAreas table
        public string Field2 { get; set; } = string.Empty;      // copied to handler (this + 3680)
        public uint Seed { get; set; }                          // DRLG seed
        public List<uint> Field4 { get; } = new();              // stuff to preload?

        public InstanceClientInstanceInformationPacket() : base(PacketId.InstanceClientInstanceInformationPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Field0));
            stream.WriteNetworkUtf16String(WorldAreaId);
            stream.WriteNetworkUtf16String(Field2);
            stream.Write(BinaryPrimitives.ReverseEndianness(Seed));

            short count = (short)Field4.Count;
            stream.Write(BinaryPrimitives.ReverseEndianness(count));
            for (int i = 0; i < count; i++)
                stream.Write(BinaryPrimitives.ReverseEndianness(Field4[i]));
        }
    }
}
