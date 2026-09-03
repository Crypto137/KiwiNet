using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    public sealed class ItemObject : GameObject, INetworkSerializable
    {
        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Template);

            // World components should probably not be allowed on item objects?
            foreach (Component component in _components)
            {
                if (component is ItemComponent itemComponent)
                    itemComponent.Serialize(connection);
            }
        }
    }
}
