using KiwiNet.Core.Extensions;

namespace KiwiNet.Protocols.Packets.Common
{
    public sealed class StringPacket : Packet
    {
        public string Value { get; set; } = string.Empty;

        public StringPacket(PacketId id) : base(id)
        {
        }

        public override string ToString()
        {
            return Value;
        }

        protected override void DeserializeData(Stream stream)
        {
            Value = stream.ReadNetworkUtf16String();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.WriteNetworkUtf16String(Value);
        }
    }
}
