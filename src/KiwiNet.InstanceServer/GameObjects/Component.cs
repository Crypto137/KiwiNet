using KiwiNet.InstanceServer.Resources.Objects;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class Component
    {
        public GameObject Owner { get; set; }   // TODO: make this private set

        public virtual void Initialize(ComponentTemplate template, GameObject owner)
        {
            Owner = owner;
        }
    }
}
