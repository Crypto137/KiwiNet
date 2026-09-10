namespace KiwiNet.InstanceServer.Objects
{
    public abstract class ComponentTemplateFactory
    {
        public ComponentTemplateFactory(ComponentTemplateRegistry registry)
        {
            registry.Factories[GetName()] = this;
        }

        public abstract ComponentTemplate Allocate(ObjectTemplate objectTemplate);

        public abstract string GetName();

        public virtual void ApplyTableData(ComponentTemplate componentTemplate, string fileName) { }
    }
}
