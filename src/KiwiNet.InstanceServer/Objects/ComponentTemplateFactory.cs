namespace KiwiNet.InstanceServer.Objects
{
    public abstract class ComponentTemplateFactory
    {
        public ComponentTemplateFactory(ComponentTemplateRegistry registry)
        {
            string name = GetName();

            if (registry.Definitions.TryGetValue(name, out ComponentTemplateDefinition definition) == false)
            {
                definition = new();
                registry.Definitions.Add(name, definition);
            }

            definition.Factory = this;
        }

        public abstract ComponentTemplate Allocate(ObjectTemplate objectTemplate);

        public abstract string GetName();

        public virtual void ApplyTableData(ComponentTemplate componentTemplate, string fileName) { }
    }
}
