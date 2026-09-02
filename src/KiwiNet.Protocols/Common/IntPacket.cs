using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Common
{
    public sealed class IntPacket : Packet
    {
        public int Value { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Value);
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Value = connection.Read<int>();
        }
    }
}
