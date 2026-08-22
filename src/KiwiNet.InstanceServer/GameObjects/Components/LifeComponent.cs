using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class LifeComponent : Component
    {
        public uint Life { get; set; } = 0;

        public override void Serialize(Stream stream)
        {
            // sub_4DE810
            PacketIO.WriteUInt32(stream, Life);
            PacketIO.WriteByte(stream, 0);
            // virtual calls in a loop, the byte above may be a size for a vector

            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
            PacketIO.WriteUInt32(stream, 0);
        }
    }
}
