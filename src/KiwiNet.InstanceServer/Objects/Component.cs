namespace KiwiNet.InstanceServer.Objects
{
    public abstract class Component
    {
        public ObjectBase Owner { get; private set; }

        /// <summary>
        /// Called when a <see cref="Component"/> is added to a <see cref="ObjectBase"/>.
        /// </summary>
        public virtual void Initialize(ComponentTemplate template, ObjectBase owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Called when the owner is being destroyed.
        /// </summary>
        public virtual void Destroy()
        {
            // return to component pool?
        }

        /// <summary>
        /// Called when the owner <see cref="ObjectBase"/> finishes initialization, including creating all components.
        /// </summary>
        public virtual void PostInitialize()
        {
        }
    }
}
