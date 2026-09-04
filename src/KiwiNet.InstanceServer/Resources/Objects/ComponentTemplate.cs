using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Resources.Objects
{
    public abstract class ComponentTemplate
    {
        public abstract Component CreateComponent(GameObject owner);

        public void PostProcess() { }
    }
}
