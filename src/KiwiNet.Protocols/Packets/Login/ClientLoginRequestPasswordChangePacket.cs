namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class ClientLoginRequestPasswordChangePacket : Packet
    {
        public byte[] OldPasswordHash { get; } = new byte[32];
        public byte[] NewPasswordHash { get; } = new byte[32];

        public ClientLoginRequestPasswordChangePacket() : base(PacketId.ClientLoginRequestPasswordChangePacketId)
        {
        }

        public override string ToString()
        {
            return $"OldPasswordHash={Convert.ToHexString(OldPasswordHash)}, NewPasswordHash={Convert.ToHexString(NewPasswordHash)}";
        }

        protected override void DeserializeData(Stream stream)
        {
            stream.Read(OldPasswordHash);
            stream.Read(NewPasswordHash);
        }
    }
}
