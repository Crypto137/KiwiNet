namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientServerFrameDurationPacket : Packet
    {
        public short ServerFrameTimeMS { get; set; }

        public InstanceClientServerFrameDurationPacket() : base(PacketId.InstanceClientServerFrameDurationPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteInt16(stream, ServerFrameTimeMS);
        }
    }
}
