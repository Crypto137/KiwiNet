using KiwiNet.InstanceServer.Resources;
using System.Diagnostics;

namespace KiwiNet.InstanceServer.Objects
{
    public abstract class ObjectBase
    {
        protected readonly List<Component> _components = new();

        public T GetComponent<T>(int index) where T: Component
        {
            Debug.Assert(index >= 0 && index < _components.Count);
            return (T)_components[index];
        }
    }

    public abstract class ObjectBase<TTemplate> : ObjectBase where TTemplate: ObjectTemplate, new()
    {
        protected ResourceHandle<TTemplate> _template;

        public override string ToString()
        {
            return _template != null ? _template.FileName : "NULL";
        }

        public T GetComponent<T>() where T : Component
        {
            TTemplate template = _template.Resource;

            if (template.ComponentIndicesByName.TryGetValue(typeof(T).Name, out int index) == false)
                return default;

            return (T)_components[index];
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

        public virtual void Destroy()
        {
            foreach (Component component in _components)
                component.Destroy();

            _components.Clear();

            if (_template != null)
            {
                _template.DecrementRefCount();
                _template = null;
            }
        }
    }
}
