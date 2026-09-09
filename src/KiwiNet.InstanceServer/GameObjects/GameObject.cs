using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class GameObject
    {
        protected readonly List<Component> _components = new();

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }

            return null;
        }
    }

    public abstract class GameObject<TTemplate> : GameObject where TTemplate: GameObjectTemplate, new()
    {
        protected ResourceHandle<TTemplate> _template;

        public virtual void Initialize(ResourceHandle<TTemplate> templateHandle)
        {
        }

        protected void InitializeComponents(ResourceHandle<TTemplate> templateHandle)
        {
            _template = templateHandle;
            templateHandle.IncrementRefCount();

            TTemplate template = templateHandle.Resource;

            _components.Clear();
            _components.EnsureCapacity(template.Components.Count);
            for (int i = 0; i < template.Components.Count; i++)
            {
                Component component = template.Components[i].CreateComponent(this);
                _components.Add(component);
            }
        }
    }
}
