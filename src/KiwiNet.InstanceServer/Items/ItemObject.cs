using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class ItemObject : GameObject<ItemObjectTemplate>, INetworkSerializable
    {
        public override void Initialize(ResourceHandle<ItemObjectTemplate> templateHandle)
        {
            InitializeComponents(templateHandle);

            foreach (Component component in _components)
                component.PostInitialize();
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(_template.Resource.Hash);

            foreach (Component component in _components)
                ((ItemComponent)component).Serialize(connection);
        }
    }
}
