using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class PlayerComponent : ComponentA
    {
        public string Name { get; set; } = string.Empty;
        public CharacterClass Class { get; set; }
        public uint Experience { get; set; }
        public List<uint> PassiveSkills { get; } = new();
        public bool IsWashedUp { get; set; }    // activates WashedUp action
        public uint Unknown { get; set; }
        public byte[] QuestStates { get; } = new byte[16];   // 4 bits per quest? 32 quests total?

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Name);
            PacketIO.WriteByte(stream, (byte)Class);
            PacketIO.WriteUInt32(stream, Experience);

            PacketIO.WriteInt32(stream, PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                PacketIO.WriteUInt32(stream, passiveSkill);

            PacketIO.WriteBool(stream, IsWashedUp);
            PacketIO.WriteUInt32(stream, Unknown);
            stream.Write(QuestStates);
        }
    }
}
