using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class LifeComponent : Component
    {
        public override void Serialize(Stream stream)
        {
            // dummy bytes
            for (int i = 0; i < 21; i++)
                PacketIO.WriteByte(stream, 0);
        }
    }
}
