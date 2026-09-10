using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects.Components;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObject : ObjectBase<WorldObjectTemplate>
    {
        private readonly List<KeyValuePair<uint, uint>> _unkList = new();

        public uint Id { get; set; }
        public PositionedComponent Positioned { get; private set; }

        public WorldObject() { }

        public override void Initialize(ResourceHandle<WorldObjectTemplate> templateHandle)
        {
            InitializeComponents(templateHandle);

            Positioned = GetComponent<PositionedComponent>();

            foreach (Component component in _components)
                component.PostInitialize();
        }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(_template.Resource.Hash);
            connection.Write(Id);

            connection.Write((byte)_unkList.Count);
            foreach (var kvp in _unkList)
            {
                connection.Write(kvp.Key);
                connection.Write(kvp.Value);
            }

            foreach (Component component in _components)
                ((WorldComponent)component).Serialize(connection);
        }
    }
}
