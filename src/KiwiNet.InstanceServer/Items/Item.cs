using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Items
{
    public sealed class Item : ObjectBase<ItemTemplate>, INetworkSerializable
    {
        public override void Initialize(ResourceHandle<ItemTemplate> templateHandle)
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
