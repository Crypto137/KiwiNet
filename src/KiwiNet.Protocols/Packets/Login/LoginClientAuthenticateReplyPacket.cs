using KiwiNet.Core.Extensions;
using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class LoginClientAuthenticateReplyPacket : Packet
    {
        public BackendError Result { get; set; }
        public ulong ProtocolHash0 { get; set; }
        public ulong ProtocolHash1 { get; set; }
        public ulong ProtocolHash2 { get; set; }
        public ulong ProtocolHash3 { get; set; }

        public LoginClientAuthenticateReplyPacket() : base(PacketId.LoginClientAuthenticateReplyPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(Result);
            stream.Write(ProtocolHash0);
            stream.Write(ProtocolHash1);
            stream.Write(ProtocolHash2);
            stream.Write(ProtocolHash3);
        }
    }
}
