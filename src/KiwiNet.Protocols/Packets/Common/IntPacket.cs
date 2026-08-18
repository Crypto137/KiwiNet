using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Common
{
    public sealed class IntPacket : Packet
    {
        public int Value { get; set; }

        public IntPacket(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Value = BinaryPrimitives.ReverseEndianness(stream.Read<int>());
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Value));
        }
    }
}
