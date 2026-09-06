namespace KiwiNet.InstanceServer.Resources.Objects
{
    public abstract class ComponentTemplateFactory
    {
        public abstract ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate);

        public abstract string GetName();

        public virtual void PopulateFromResource(ComponentTemplate componentTemplate) { }
    }
}
