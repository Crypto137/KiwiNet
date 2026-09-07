namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class ComponentTemplate
    {
        protected readonly GameObjectTemplate _gameObjectTemplate;

        public ComponentTemplate(GameObjectTemplate gameObjectTemplate)
        {
            _gameObjectTemplate = gameObjectTemplate;
        }

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

        public virtual void GetDependencies(List<string> dependencies)
        {
        }

        public virtual void PostProcess()
        {
        }
    }

    public abstract class ComponentTemplate<T> : ComponentTemplate where T : Component, new()
    {
        public ComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }

        public override Component CreateComponent(GameObject owner)
        {
            T component = new();
            component.Initialize(this, owner);
            return component;
        }
    }
}
