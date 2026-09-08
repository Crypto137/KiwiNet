using KiwiNet.Core.Logging;

namespace KiwiNet.InstanceServer.GameObjects
{
    public abstract class ComponentTemplate
    {
        // FIXME: clean up debug logging and return false for non-existent variables when everything is working properly
        private static readonly Logger Logger = LogManager.CreateLogger();

        protected readonly GameObjectTemplate _gameObjectTemplate;

        public ComponentTemplate(GameObjectTemplate gameObjectTemplate)
        {
            _gameObjectTemplate = gameObjectTemplate;
        }

        public abstract Component CreateComponent(GameObject owner);

        public virtual bool SetBoolVariable(string name, bool value)
        {
            Logger.Debug($"{GetType().Name}.SetBoolVariable(): {name} = {value}");
            return true;
        }

        public virtual bool SetFloatVariable(string name, float value)
        {
            Logger.Debug($"{GetType().Name}.SetFloatVariable(): {name} = {value}");
            return true;
        }

        public virtual bool SetStringVariable(string name, string value)
        {
            Logger.Debug($"{GetType().Name}.SetStringVariable(): {name} = {value}");
            return true;
        }

        public virtual bool SetIntVariable(string name, int value)
        {
            Logger.Debug($"{GetType().Name}.SetIntVariable(): {name} = {value}");
            return true;
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
