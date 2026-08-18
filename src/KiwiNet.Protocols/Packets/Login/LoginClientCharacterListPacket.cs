using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

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
            stream.WriteNetworkUtf16String(Name);
            stream.WriteNetworkUtf16String(League);
            stream.Write(Field2);
            stream.Write(BinaryPrimitives.ReverseEndianness(Level));
            stream.Write(BinaryPrimitives.ReverseEndianness(Field4));
            stream.Write((byte)Class);
        }
    }

    public sealed class LoginClientCharacterListPacket : Packet
    {
        public List<CharacterInfo> Characters { get; } = new();
        public int Field1 { get; set; }

        public LoginClientCharacterListPacket() : base(PacketId.LoginClientCharacterListPacketId)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(Characters.Count));
            foreach (CharacterInfo character in Characters)
                character.Serialize(stream);

            stream.Write(BinaryPrimitives.ReverseEndianness(Field1));
        }
    }
}
