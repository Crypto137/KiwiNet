using KiwiNet.InstanceServer.Entities.Components;
using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.Entities
{
    public class Entity
    {
        private readonly List<KeyValuePair<uint, uint>> _unkList = new();
        private readonly List<Component> _components = new();

        public uint Template { get; private set; }
        public uint Id { get; private set; }

        public Entity()
        {
        }

        public void Initialize(ref EntitySettings settings)
        {
            Template = settings.Template;
            Id = settings.Id;

            PositionedComponent positioned = GetOrCreateComponent<PositionedComponent>();
            positioned.X = settings.PositionX;
            positioned.Y = settings.PositionY;
        }

        public T GetOrCreateComponent<T>() where T: Component, new()
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

        public T GetComponent<T>() where T: Component
        {
            foreach (Component component in _components)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }

            return null;
        }

        public void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Template);
            PacketIO.WriteUInt32(stream, Id);   // probably id, not sure yet

            PacketIO.WriteByte(stream, (byte)_unkList.Count);

            if (_unkList.Count > 0)
            {
                foreach (var kvp in _unkList)
                {
                    PacketIO.WriteUInt32(stream, kvp.Key);
                    PacketIO.WriteUInt32(stream, kvp.Value);
                }
            }
            else
            {
                foreach (Component component in _components)
                    component.Serialize(stream);
            }
        }
    }
}
