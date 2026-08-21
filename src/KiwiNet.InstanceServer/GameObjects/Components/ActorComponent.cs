using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class ActorComponent : Component
    {
        public override void Serialize(Stream stream)
        {
            // call Serialize2 (aka probably SerializeUpdate)
            PacketIO.WriteByte(stream, 0);

            bool hasExtraData = false;
            PacketIO.WriteBool(stream, hasExtraData);
            if (hasExtraData)
            {
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteUInt32(stream, 0);

                PacketIO.WriteInt16(stream, 0);

                PacketIO.WriteInt16(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteByte(stream, 0);
            }

            PacketIO.WriteByte(stream, 0);

            // these look like counts
            PacketIO.WriteInt16(stream, 0);
            PacketIO.WriteInt16(stream, 0);
        }
    }
}
