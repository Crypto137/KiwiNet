using KiwiNet.Core.Network;

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

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Value);
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Value = connection.ReadString();
        }
    }
}
