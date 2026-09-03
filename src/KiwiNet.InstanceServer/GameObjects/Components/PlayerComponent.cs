using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

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

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write((byte)Class);
            connection.Write(Experience);

            connection.Write(PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                connection.Write(passiveSkill);

            connection.Write(IsWashedUp);
            connection.Write(Unknown);
            connection.Write(QuestStates);
        }
    }
}
