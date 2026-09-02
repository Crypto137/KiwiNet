using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class ClientLoginAuthenticatePacket : Packet
    {
        public uint Field0 { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; } = new byte[32];

        public ClientLoginAuthenticatePacket() : base(PacketId.ClientLoginAuthenticatePacketId)
        {
        }

        public override string ToString()
        {
            return $"Field0=0x{Field0:X2}, Email={Email}, PasswordHash={Convert.ToHexString(PasswordHash)}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<uint>();
            Email = connection.ReadString();
            connection.Read(PasswordHash);
        }
    }
}
