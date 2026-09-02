using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientLoginAttemptReplyPacket : Packet
    {
        public uint Field0 { get; set; }
        public string Field1 { get; set; } = string.Empty;

        public InstanceClientLoginAttemptReplyPacket() : base(PacketId.InstanceClientLoginAttemptReplyPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
        }
    }
}
