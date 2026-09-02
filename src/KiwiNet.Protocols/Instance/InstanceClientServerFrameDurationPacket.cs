using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientServerFrameDurationPacket : Packet
    {
        public short ServerFrameTimeMS { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(ServerFrameTimeMS);
        }
    }
}
