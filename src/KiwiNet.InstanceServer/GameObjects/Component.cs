namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class Component
    {
        public GameObject Owner { get; private set; }

        /// <summary>
        /// Called when a <see cref="Component"/> is added to a <see cref="GameObject"/>.
        /// </summary>
        public virtual void Initialize(ComponentTemplate template, GameObject owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Called when the owner <see cref="GameObject"/> finishes initialization, including creating all components.
        /// </summary>
        public virtual void PostInitialize()
        {
        }
    }
}
