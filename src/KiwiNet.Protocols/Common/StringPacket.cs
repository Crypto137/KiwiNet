using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Common
{
    public sealed class StringPacket : Packet
    {
        public string Value { get; set; } = string.Empty;

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
