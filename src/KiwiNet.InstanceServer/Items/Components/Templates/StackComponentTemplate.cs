using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class StackComponentTemplate : ComponentTemplate<StackComponent>
    {
        public StackComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class StackComponentTemplateFactory : ComponentTemplateFactory
    {
        public StackComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new StackComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Stack";
        }
    }
}
