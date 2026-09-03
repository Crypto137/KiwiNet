using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class InventoriesComponent : ComponentA
    {
        public const int NumInventories = 37;

        public override void Serialize(NetworkConnection connection)
        {
            for (int i = 0; i < NumInventories; i++)
            {
                int count = 0;
                connection.Write(count);
                for (int j = 0; j < count; j++)
                {
                    // TODO: item serialization for each inventory
                }
            }
        }
    }
}
