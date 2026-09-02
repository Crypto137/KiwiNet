using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientServerFrameDurationPacket : Packet
    {
        public short ServerFrameTimeMS { get; set; }

        public InstanceClientServerFrameDurationPacket() : base(PacketId.InstanceClientServerFrameDurationPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(ServerFrameTimeMS);
        }
    }
}
