using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.GameObjects
{
    public sealed class WorldObject : GameObject
    {
        private readonly List<KeyValuePair<uint, uint>> _unkList = new();

        private PositionedComponent _positioned;

        public uint Id { get; private set; }

        public override void Initialize(ref GameObjectSettings settings)
        {
            base.Initialize(ref settings);

            Id = settings.Id;

            // The client looks up the Positioned component and saves a pointer to it in the constructor,
            // indicating that all world objects are probably expected to have it.
            // It appears the client does some kind of component name -> vector index lookup
            _positioned = GetOrCreateComponent<PositionedComponent>();
            _positioned.SetPosition(settings.GridPosition);
            _positioned.Rotation = settings.Rotation;
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Template);
            connection.Write(Id);

            connection.Write((byte)_unkList.Count);
            foreach (var kvp in _unkList)
            {
                connection.Write(kvp.Key);
                connection.Write(kvp.Value);
            }

            foreach (Component component in _components)
            {
                // Not sure yet if item components are allowed on world objects
                if (component is WorldComponent worldComponent)
                    worldComponent.Serialize(connection);
            }
        }
    }
}
