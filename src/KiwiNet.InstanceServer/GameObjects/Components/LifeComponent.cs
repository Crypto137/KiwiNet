using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public readonly struct BuffEntry
    {
        public readonly uint Field0;

        public readonly uint Field1;
        public readonly uint Field2;
        public readonly uint Field3;
        public readonly uint Field4;
        public readonly uint Field5;
        public readonly List<uint> Values1;

        public readonly List<uint> Values2;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);

            PacketIO.WriteUInt32(stream, Field1);
            PacketIO.WriteUInt32(stream, Field2);
            PacketIO.WriteUInt32(stream, Field3);
            PacketIO.WriteUInt32(stream, Field4);
            PacketIO.WriteUInt32(stream, Field5);
            // count for the first list of values appears to be not serialized and taken from buff definition
            foreach (uint value in Values1)
                PacketIO.WriteUInt32(stream, value);

            PacketIO.WriteInt32(stream, Values2.Count);
            foreach (uint value in Values2)
                PacketIO.WriteUInt32(stream, value);
        }
    }

    public sealed class LifeComponent : Component
    {
        public uint Life { get; set; }
        public uint Mana { get; set; }
        public uint EnergyShield { get; set; }
        public uint UnkField { get; set; }  // serialized with ES, probably related

        public byte UnkFlag { get; set; }

        public List<BuffEntry> Buffs { get; } = new();

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Life);
            PacketIO.WriteUInt32(stream, Mana);
            PacketIO.WriteUInt32(stream, EnergyShield);
            PacketIO.WriteUInt32(stream, UnkField);

            PacketIO.WriteByte(stream, UnkFlag);

            PacketIO.WriteInt32(stream, Buffs.Count);
            foreach (BuffEntry buff in Buffs)
                buff.Serialize(stream);
        }
    }
}
