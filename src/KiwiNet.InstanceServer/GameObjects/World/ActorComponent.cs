using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.World
{
    [Flags]
    public enum ActorStructExtraFlags : short
    {
        None        = 0,
        Flag0       = 1 << 0,
        Flag1       = 1 << 1,
        Flag2       = 1 << 2,
        Flag3       = 1 << 3,
        Flag4       = 1 << 4,
        Flag5       = 1 << 5,
        Flag6       = 1 << 6,
        Flag7       = 1 << 7,
        Flag8       = 1 << 8,
        Flag9       = 1 << 9,
    }

    public readonly struct ActorStruct
    {
        public readonly short Field0;
        public readonly byte Field2;
        public readonly byte Field3;
        public readonly uint Field4;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field3); // <- byte at offset 3 is written before 2, this is client-accurate
            connection.Write(Field2);
            connection.Write(Field4);
        }
    }

    public readonly struct ActorStructWithExtra
    {
        public readonly uint Field0;
        public readonly uint Field4;
        public readonly uint Field8;
        public readonly uint Field12;
        public readonly uint Field16;
        public readonly short Field20;
        public readonly ActorStructExtraFlags Flags;
        public readonly ActorStruct ActorStruct24;
        public readonly byte Field32;

        public void Serialize(NetworkConnection connection)
        {
            ActorStruct24.Serialize(connection);
            connection.Write((short)Flags);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag9))
                connection.Write(Field20);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag1))
                connection.Write(Field4);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag2))
                connection.Write(Field8);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag3))
                connection.Write(Field12);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag4))
                connection.Write(Field16);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag0))
                connection.Write(Field0);

            connection.Write(Field32);
        }
    }

    public sealed class ActorComponent : WorldComponent
    {
        public List<ActorStructWithExtra> DataList1 { get; } = new();
        public List<ActorStruct> DataList2 { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            SerializeUpdate(connection);
        }

        public override void SerializeUpdate(NetworkConnection connection)
        {
            connection.Write((byte)0);

            bool hasSkillData = false;
            connection.Write(hasSkillData);
            if (hasSkillData)
            {
                connection.Write(300);  // grid x
                connection.Write(540);  // grid y

                connection.Write((ushort)0xB188);   // skill hash
                connection.Write((ushort)0);
                connection.Write(0);                // can be 0
                connection.Write(0);
                connection.Write(0);
                connection.Write((byte)0);
            }

            connection.Write((byte)0);

            connection.Write((short)DataList1.Count);
            foreach (ActorStructWithExtra entry in DataList1)
                entry.Serialize(connection);

            connection.Write((short)DataList2.Count);
            foreach (ActorStruct entry in DataList2)
                entry.Serialize(connection);
        }
    }
}
