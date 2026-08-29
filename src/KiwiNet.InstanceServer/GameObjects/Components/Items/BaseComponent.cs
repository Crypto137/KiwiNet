using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components.Items
{
    public sealed class BaseComponent : ComponentB
    {
        public override void Serialize(Stream stream)
        {
            PacketIO.WriteByte(stream, 0);
        }

        public override void Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
