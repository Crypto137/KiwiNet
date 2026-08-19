namespace KiwiNet.Protocols.Packets.Login
{
    public readonly struct LeagueInfo(string name, string description, bool isHardcore)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly bool IsHardcore = isHardcore;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Name);
            PacketIO.WriteString(stream, Description);
            PacketIO.WriteBool(stream, IsHardcore);
        }
    }

    public sealed class LoginClientLeagueListPacket : Packet
    {
        public List<LeagueInfo> Leagues { get; } = new();

        public LoginClientLeagueListPacket() : base(PacketId.LoginClientLeagueListPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteInt32(stream, Leagues.Count);
            foreach (LeagueInfo league in Leagues)
                league.Serialize(stream);
        }
    }
}
