using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectAddPacket : Packet
    {
        public uint ObjectTemplate { get; set; }                // MurmurHash2 reference to a file in GGPK
        public uint Field1 { get; set; }                        // runtime id?
        public List<(uint, uint)> Field2 { get; } = new();
        public byte[] Blob { get; set; } = Array.Empty<byte>();

        public InstanceClientObjectAddPacket() : base((PacketId)100)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(ObjectTemplate));
            stream.Write(BinaryPrimitives.ReverseEndianness(Field1));

            stream.Write((byte)Field2.Count);
            foreach (var kvp in Field2)
            {
                stream.Write(BinaryPrimitives.ReverseEndianness(kvp.Item1));
                stream.Write(BinaryPrimitives.ReverseEndianness(kvp.Item2));
            }

            stream.Write(Blob);
        }
    }
}
