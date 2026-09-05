using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Resources.Objects
{
    public abstract class ComponentTemplate
    {
        public abstract Component CreateComponent(GameObject owner);

        public virtual bool SetBoolVariable(string name, bool value)
        {
            return false;
        }

        public virtual bool SetFloatVariable(string name, float value)
        {
            return false;
        }

        public virtual bool SetStringVariable(string name, string value)
        {
            return false;
        }

        public virtual bool SetIntVariable(string name, int value)
        {
            return false;
        }

        public virtual void PostProcess()
        {
        }
    }
}
