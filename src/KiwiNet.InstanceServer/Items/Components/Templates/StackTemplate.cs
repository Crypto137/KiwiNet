using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class StackTemplate : ComponentTemplate<Stack>
    {
        public StackTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class StackTemplateFactory : ComponentTemplateFactory
    {
        public StackTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new StackTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Stack);
        }
    }
}
