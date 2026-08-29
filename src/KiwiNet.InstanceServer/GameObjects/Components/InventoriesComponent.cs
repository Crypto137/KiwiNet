using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class InventoriesComponent : ComponentA
    {
        public const int NumInventories = 37;

        public override void Serialize(Stream stream)
        {
            for (int i = 0; i < NumInventories; i++)
            {
                int count = 0;
                PacketIO.WriteInt32(stream, count);
                for (int j = 0; j < count; j++)
                {
                    // TODO: item serialization for each inventory
                }
            }
        }
    }
}
