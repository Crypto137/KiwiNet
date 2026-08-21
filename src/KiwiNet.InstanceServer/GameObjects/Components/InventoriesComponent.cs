using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class InventoriesComponent : Component
    {
        public override void Serialize(Stream stream)
        {
            // dummy bytes
            for (int i = 0; i < 148; i++)
                PacketIO.WriteByte(stream, 0);
        }
    }
}
