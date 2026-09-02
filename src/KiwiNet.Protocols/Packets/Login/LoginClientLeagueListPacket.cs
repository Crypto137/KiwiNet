using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Login
{
    public readonly struct LeagueInfo(string name, string description, bool isHardcore)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly bool IsHardcore = isHardcore;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write(Description);
            connection.Write(IsHardcore);
        }
    }

    public sealed class LoginClientLeagueListPacket : Packet
    {
        public List<LeagueInfo> Leagues { get; } = new();

        public LoginClientLeagueListPacket() : base(PacketId.LoginClientLeagueListPacketId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Leagues.Count);
            foreach (LeagueInfo league in Leagues)
                league.Serialize(connection);
        }
    }
}
