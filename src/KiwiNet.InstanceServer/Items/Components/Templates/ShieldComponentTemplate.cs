using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ShieldComponentTemplate : ComponentTemplate<ShieldComponent>
    {
        public ShieldComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ShieldComponentTemplateFactory : ComponentTemplateFactory
    {
        public ShieldComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ShieldComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Shield";
        }
    }
}
