using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientLoginAttemptReplyPacket : Packet
    {
        public uint Field0 { get; set; }
        public string Field1 { get; set; } = string.Empty;

        public InstanceClientLoginAttemptReplyPacket() : base(PacketId.InstanceClientLoginAttemptReplyPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Field0));
            stream.WriteNetworkUtf16String(Field1);
        }
    }
}
