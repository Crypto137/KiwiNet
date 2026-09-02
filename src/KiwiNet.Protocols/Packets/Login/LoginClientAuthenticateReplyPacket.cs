using KiwiNet.Core.Network;
using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class LoginClientAuthenticateReplyPacket : Packet
    {
        public BackendError Result { get; set; }
        public byte[] ProtocolHash { get; } = new byte[32];

        public LoginClientAuthenticateReplyPacket() : base(PacketId.LoginClientAuthenticateReplyPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Result);
            connection.Write(ProtocolHash);
        }
    }
}
