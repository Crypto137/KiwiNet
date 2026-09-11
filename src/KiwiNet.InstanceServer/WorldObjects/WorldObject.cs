using KiwiNet.Core.Network;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects.Components;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObject : ObjectBase<WorldObjectTemplate>
    {
        private readonly List<KeyValuePair<uint, uint>> _unkList = new();

        public Area Area { get; private set; }
        public uint Id { get; set; }
        public bool Destroyed { get; private set; }
        public bool Attackable { get; set; }
        public Positioned Positioned { get; private set; }

        public WorldObject() { }

        public override string ToString()
        {
            return $"[{Id}] {_template.FileName}";
        }

        public void Initialize(ResourceHandle<WorldObjectTemplate> templateHandle, Area area)
        {
            InitializeComponents(templateHandle);

            Area = area;

            Positioned = GetComponent<Positioned>();

            Area.ObjectManager.AddObject(this);

            foreach (Component component in _components)
                component.PostInitialize();
        }

        public override void Destroy()
        {
            Destroyed = true;

            Area.ObjectManager.RemoveObject(Id);

            base.Destroy();
        }

        public void Wake()
        {
            Area.ObjectManager.WakeObject(Id);
        }

        public void Sleep()
        {
            Area.ObjectManager.SleepObject(Id);
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
