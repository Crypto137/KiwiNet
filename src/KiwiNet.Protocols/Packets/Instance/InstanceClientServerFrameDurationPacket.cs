using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientServerFrameDurationPacket : Packet
    {
        public ushort Field0 { get; set; }

        public InstanceClientServerFrameDurationPacket() : base(PacketId.InstanceClientServerFrameDurationPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Field0));
        }
    }
}
