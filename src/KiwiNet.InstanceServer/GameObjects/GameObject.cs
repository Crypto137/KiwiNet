namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class GameObject
    {
        protected readonly List<Component> _components = new();

        public uint Template { get; private set; }

        public GameObject()
        {
        }

        public virtual void Initialize(ref GameObjectSettings settings)
        {
            Template = settings.Template;
        }

        public T GetOrCreateComponent<T>() where T: Component, new()
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
    }
}
