namespace KiwiNet.LoginServer.Accounts
{
    public class Account
    {
        public ulong Id { get; init; }
        public string Email { get; init; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }

        public List<Character> Characters { get; init; } = new();

        public Account(ulong id, string email, string name, byte[] passwordHash)
        {
            Id = id;
            Email = email;
            Name = name;
            PasswordHash = passwordHash;
        }

        public override string ToString()
        {
            return $"{Name} ({Email})";
        }

        public Character GetCharacter(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            foreach (Character character in Characters)
            {
                if (string.Equals(name, character.Name, StringComparison.OrdinalIgnoreCase))
                    return character;
            }

            return null;
        }
    }
}
