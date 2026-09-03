using KiwiNet.Core.Network;

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

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);

            connection.Write(Field1);
            connection.Write(Field2);
            connection.Write(Field3);
            connection.Write(Field4);
            connection.Write(Field5);
            // count for the first list of values appears to be not serialized and taken from buff definition
            foreach (uint value in Values1)
                connection.Write(value);

            connection.Write(Values2.Count);
            foreach (uint value in Values2)
                connection.Write(value);
        }
    }

    public sealed class LifeComponent : ComponentA
    {
        public uint Life { get; set; }
        public uint Mana { get; set; }
        public uint EnergyShield { get; set; }
        public uint UnkField { get; set; }  // serialized with ES, probably related

        public byte UnkFlag { get; set; }

        public List<BuffEntry> Buffs { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Life);
            connection.Write(Mana);
            connection.Write(EnergyShield);
            connection.Write(UnkField);

            connection.Write(UnkFlag);

            connection.Write(Buffs.Count);
            foreach (BuffEntry buff in Buffs)
                buff.Serialize(connection);
        }
    }
}
