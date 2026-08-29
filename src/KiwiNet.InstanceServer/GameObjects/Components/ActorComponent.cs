using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
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

        public void Serialize(Stream stream)
        {
            PacketIO.WriteInt16(stream, Field0);
            PacketIO.WriteByte(stream, Field3); // <- byte at offset 3 is written before 2, this is client-accurate
            PacketIO.WriteByte(stream, Field2);
            PacketIO.WriteUInt32(stream, Field4);
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

        public void Serialize(Stream stream)
        {
            ActorStruct24.Serialize(stream);
            PacketIO.WriteInt16(stream, (short)Flags);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag9))
                PacketIO.WriteInt16(stream, Field20);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag1))
                PacketIO.WriteUInt32(stream, Field4);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag2))
                PacketIO.WriteUInt32(stream, Field8);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag3))
                PacketIO.WriteUInt32(stream, Field12);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag4))
                PacketIO.WriteUInt32(stream, Field16);

            if (Flags.HasFlag(ActorStructExtraFlags.Flag0))
                PacketIO.WriteUInt32(stream, Field0);

            PacketIO.WriteByte(stream, Field32);
        }
    }

    public sealed class ActorComponent : ComponentA
    {
        public List<ActorStructWithExtra> DataList1 { get; } = new();
        public List<ActorStruct> DataList2 { get; } = new();

        public override void Serialize(Stream stream)
        {
            // call Serialize2 (aka probably SerializeUpdate)
            PacketIO.WriteByte(stream, 0);

            bool hasSkillData = false;
            PacketIO.WriteBool(stream, hasSkillData);
            if (hasSkillData)
            {
                PacketIO.WriteUInt32(stream, 300);  // grid x
                PacketIO.WriteUInt32(stream, 540);  // grid y

                PacketIO.WriteInt16(stream, unchecked((short)0xB188));  // skill id
                PacketIO.WriteInt16(stream, 0);
                PacketIO.WriteUInt32(stream, 0);    // can be 0
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteByte(stream, 0);
            }

            PacketIO.WriteByte(stream, 0);

            PacketIO.WriteInt16(stream, (short)DataList1.Count);
            foreach (ActorStructWithExtra entry in DataList1)
                entry.Serialize(stream);

            PacketIO.WriteInt16(stream, (short)DataList2.Count);
            foreach (ActorStruct entry in DataList2)
                entry.Serialize(stream);
        }
    }
}
