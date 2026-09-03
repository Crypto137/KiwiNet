using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects
{
    // not sure how items fit into the game object hierarchy yet
    public class ItemData : INetworkSerializable
    {
        private List<ComponentB> _components = new();

        public uint Template { get; }

        public ItemData(uint template)
        {
            Template = template;
        }

        public T GetOrCreateComponent<T>() where T : ComponentB, new()
        {
            // temp stuff just for testing now
            foreach (Component existingComponent in _components)
            {
                if (existingComponent is T typedComponent)
                    return typedComponent;
            }

            T component = new();
            _components.Add(component);
            return component;
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Template);
            foreach (ComponentB component in _components)
                component.Serialize(connection);
        }
    }
}
