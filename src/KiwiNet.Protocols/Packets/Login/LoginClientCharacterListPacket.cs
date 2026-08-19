namespace KiwiNet.Protocols.Packets.Login
{
    public enum CharacterClass
    {
        StrDexInt,
        Str,
        Dex,
        Int,
        StrDex,
        StrInt,
        DexInt,
        NumClasses, // there are more "test" class entries that are valid, but we're not including them here yet
    }

    public readonly struct CharacterInfo(string name, string league, byte field2, int level, int field4, CharacterClass @class)
    {
        public readonly string Name = name;
        public readonly string League = league;
        public readonly byte Field2 = field2;
        public readonly int Level = level;
        public readonly int Field4 = field4;
        public readonly CharacterClass Class = @class;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Name);
            PacketIO.WriteString(stream, League);
            PacketIO.WriteUInt8(stream, Field2);
            PacketIO.WriteInt32(stream, Level);
            PacketIO.WriteInt32(stream, Field4);
            PacketIO.WriteUInt8(stream, (byte)Class);
        }
    }

    public sealed class LoginClientCharacterListPacket : Packet
    {
        public List<CharacterInfo> Characters { get; } = new();
        public int Field1 { get; set; }

        public LoginClientCharacterListPacket() : base(PacketId.LoginClientCharacterListPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteInt32(stream, Characters.Count);
            foreach (CharacterInfo character in Characters)
                character.Serialize(stream);

            PacketIO.WriteInt32(stream, Field1);
        }
    }
}
