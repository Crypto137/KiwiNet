using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class AnimatedComponent : Component
    {
        public override void Serialize(Stream stream)
        {
            // dummy bytes
            PacketIO.WriteByte(stream, 0);
        }
    }
}
