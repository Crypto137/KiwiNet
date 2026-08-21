using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class PlayerComponent : Component
    {
        public string Name { get; set; } = string.Empty;
        public List<uint> PassiveSkills { get; } = new();

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Name);

            // 30 bytes

            PacketIO.WriteByte(stream, 0);
            PacketIO.WriteUInt32(stream, 0);

            PacketIO.WriteInt32(stream, PassiveSkills.Count);
            foreach (uint passiveSkill in PassiveSkills)
                PacketIO.WriteUInt32(stream, passiveSkill);

            PacketIO.WriteByte(stream, 0);
            // WashedUp

            PacketIO.WriteUInt32(stream, 0);

            // one read of 16 bytes
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
        }
    }
}
