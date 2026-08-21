using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.Entities.Components
{
    public sealed class PositionedComponent : Component
    {
        public uint X { get; set; }     // todo: vector struct
        public uint Y { get; set; }

        public override void Serialize(Stream stream)
        {
            byte flags = 0;

            PacketIO.WriteUInt32(stream, X);
            PacketIO.WriteUInt32(stream, Y);
            PacketIO.WriteUInt32(stream, 0);    // float rotation?
            PacketIO.WriteByte(stream, flags);
            PacketIO.WriteUInt32(stream, 0);

            if ((flags & 0x4) != 0)
            {
                PacketIO.WriteUInt32(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
            }
        }
    }
}
