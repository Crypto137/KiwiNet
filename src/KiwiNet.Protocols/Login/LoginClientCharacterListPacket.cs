using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

namespace KiwiNet.Protocols.Login
{
    public readonly struct CharacterInfo(string name, string league, byte field2, int level, int field4, CharacterClass @class)
    {
        public readonly string Name = name;
        public readonly string League = league;
        public readonly byte Field2 = field2;
        public readonly int Level = level;
        public readonly int Field4 = field4;
        public readonly CharacterClass Class = @class;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write(League);
            connection.Write(Field2);
            connection.Write(Level);
            connection.Write(Field4);
            connection.Write((byte)Class);
        }
    }

    public sealed class LoginClientCharacterListPacket : Packet
    {
        public List<CharacterInfo> Characters { get; } = new();
        public int Field1 { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Characters.Count);
            foreach (CharacterInfo character in Characters)
                character.Serialize(connection);
            connection.Write(Field1);
        }
    }
}
