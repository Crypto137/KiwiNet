using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Common
{
    /// <summary>
    /// Represents a packet with no data outside of its id.
    /// </summary>
    public sealed class SimplePacket : Packet
    {
        public SimplePacket(PacketId id) : base(id)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
        }
    }
}
