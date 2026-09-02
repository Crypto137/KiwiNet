using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Common
{
    /// <summary>
    /// Represents a packet with no data outside of its id.
    /// </summary>
    public sealed class SimplePacket : Packet
    {
        public override void Serialize(NetworkConnection connection)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
        }
    }
}
