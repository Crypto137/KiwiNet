using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Common
{
    public sealed class BackendErrorPacket : Packet
    {
        public BackendError Value { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Value);
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Value = (BackendError)connection.Read<byte>();
        }
    }
}
