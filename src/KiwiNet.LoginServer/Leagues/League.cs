using KiwiNet.Protocols.Packets.Login;

namespace KiwiNet.LoginServer.Leagues
{
    public class League
    {
        public ulong Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHardcore { get; set; }

        public League(ulong id, string name, string description, bool isHardcore)
        {
            Id = id;
            Name = name;
            Description = description;
            IsHardcore = isHardcore;
        }

        public override string ToString()
        {
            return Name;
        }

        public LeagueInfo GetLeagueInfo()
        {
            return new(Name, Description, IsHardcore);
        }
    }
}
