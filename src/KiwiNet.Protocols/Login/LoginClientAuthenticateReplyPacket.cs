using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

namespace KiwiNet.Protocols.Login
{
    public sealed class LoginClientAuthenticateReplyPacket : Packet
    {
        public BackendError Result { get; set; }
        public byte[] ProtocolHash { get; } = new byte[32];

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Result);
            connection.Write(ProtocolHash);
        }
    }
}
