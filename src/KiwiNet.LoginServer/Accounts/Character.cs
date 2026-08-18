using KiwiNet.Protocols.Packets.Login;

namespace KiwiNet.LoginServer.Accounts
{
    public class Character
    {
        public ulong Id { get; init; }
        public string Name { get; set; }
        public string League { get; set; }
        public int Level { get; set; }
        public CharacterClass Class { get; set; }

        public Character(ulong id, string name, string league, CharacterClass @class)
        {
            Id = id;
            Name = name;
            League = league;
            Level = 1;
            Class = @class;
        }

        public override string ToString()
        {
            return Name;
        }

        public CharacterInfo GetCharacterInfo()
        {
            return new(Name, League, 0, Level, 0, Class);
        }
    }
}
