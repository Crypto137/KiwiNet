using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    [Flags]
    public enum PlayerUpdateFlags : byte
    {
        None            = 0,
        PassiveSkills   = 1 << 0,
        Experience      = 1 << 1,
        QuestState      = 1 << 2,
    }

    public sealed class Player : WorldComponent
    {
        private int _experienceDelta;
        private bool _passiveSkillsChanged;
        private bool _questStateChanged;

        public string Name { get; set; } = string.Empty;
        public CharacterClass Class { get; set; }
        public uint Experience { get; set; }
        public HashSet<uint> PassiveSkills { get; } = new();
        public bool IsWashedUp { get; set; }    // activates WashedUp action
        public uint Unknown { get; set; }
        public byte[] QuestStates { get; } = new byte[16];   // 4 bits per quest? 32 quests total?

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Name);
            connection.Write((byte)Class);
            connection.Write(Experience);
            SerializePassiveSkills(connection);
            connection.Write(IsWashedUp);
            connection.Write(Unknown);
            connection.Write(QuestStates);
        }

        public override void SerializeUpdate(NetworkConnection connection)
        {
            PlayerUpdateFlags flags = PlayerUpdateFlags.None;

            if (_passiveSkillsChanged)
                flags |= PlayerUpdateFlags.PassiveSkills;

            if (_experienceDelta != 0)
                flags |= PlayerUpdateFlags.Experience;

            if (_questStateChanged)
                flags |= PlayerUpdateFlags.QuestState;

            connection.Write((byte)flags);

            if (_passiveSkillsChanged)
                SerializePassiveSkills(connection);

            if (_experienceDelta != 0)
                connection.Write(_experienceDelta);

            if (_questStateChanged)
                connection.Write(QuestStates);
        }

        public override void ResetUpdate()
        {
            _experienceDelta = 0;
            _passiveSkillsChanged = false;
            _questStateChanged = false;
        }

        public void AllocatePassiveSkillPoint(uint passiveSkill)
        {
            if (PassiveSkills.Add(passiveSkill))
                _passiveSkillsChanged = true;
        }

        public void AdjustExperience(int amount)
        {
            long experience = Experience;
            experience += amount;
            experience = Math.Clamp(experience, 0, uint.MaxValue);
            Experience = (uint)experience;

            _experienceDelta += amount;
        }

        private void SerializePassiveSkills(NetworkConnection connection)
        {
            connection.Write(PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                connection.Write(passiveSkill);
        }
    }
}
