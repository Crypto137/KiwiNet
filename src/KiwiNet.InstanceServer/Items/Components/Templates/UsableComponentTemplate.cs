using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class UsableComponentTemplate : ComponentTemplate<UsableComponent>
    {
        public UsableComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class UsableComponentTemplateFactory : ComponentTemplateFactory
    {
        public UsableComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new UsableComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Usable";
        }
    }
}
