using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.GameObjects.Components;

namespace KiwiNet.InstanceServer.GameObjects
{
    public class GameObject
    {
        private readonly List<KeyValuePair<uint, uint>> _unkList = new();
        private readonly List<ComponentA> _components = new();

        public uint Template { get; private set; }
        public uint Id { get; private set; }

        public GameObject()
        {
        }

        public void Initialize(ref GameObjectSettings settings)
        {
            Template = settings.Template;
            Id = settings.Id;

            PositionedComponent positioned = GetOrCreateComponent<PositionedComponent>();
            positioned.SetPosition(settings.GridPosition);
            positioned.Rotation = settings.Rotation;
        }

        public T GetOrCreateComponent<T>() where T: ComponentA, new()
        {
            // temp stuff just for testing now
            foreach (Component existingComponent in _components)
            {
                if (existingComponent is T typedComponent)
                    return typedComponent;
            }

            T component = new() { Owner = this };
            _components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T: Component
        {
            foreach (Component component in _components)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }

            return null;
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Template);
            connection.Write(Id);

            connection.Write((byte)_unkList.Count);

            if (_unkList.Count > 0)
            {
                foreach (var kvp in _unkList)
                {
                    connection.Write(kvp.Key);
                    connection.Write(kvp.Value);
                }
            }
            else
            {
                foreach (ComponentA component in _components)
                    component.Serialize(connection);
            }
        }
    }
}
