namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientLoginAttemptReplyPacket : Packet
    {
        public uint Field0 { get; set; }
        public string Field1 { get; set; } = string.Empty;

        public InstanceClientLoginAttemptReplyPacket() : base(PacketId.InstanceClientLoginAttemptReplyPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteString(stream, Field1);
        }
    }
}
