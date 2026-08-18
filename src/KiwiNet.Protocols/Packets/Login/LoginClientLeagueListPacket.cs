using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Login
{
    public readonly struct LeagueInfo(string name, string description, bool isHardcore)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly bool IsHardcore = isHardcore;

        public void Serialize(Stream stream)
        {
            stream.WriteNetworkUtf16String(Name);
            stream.WriteNetworkUtf16String(Description);
            stream.Write(IsHardcore);
        }
    }

    public sealed class LoginClientLeagueListPacket : Packet
    {
        public List<LeagueInfo> Leagues { get; } = new();

        public LoginClientLeagueListPacket() : base(PacketId.LoginClientLeagueListPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Leagues.Count));
            foreach (LeagueInfo league in Leagues)
                league.Serialize(stream);
        }
    }
}
