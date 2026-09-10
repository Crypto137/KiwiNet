using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class BaseComponentTemplate : ComponentTemplate<BaseComponent>
    {
        public BaseComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class BaseComponentTemplateFactory : ComponentTemplateFactory
    {
        public BaseComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new BaseComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Base";
        }
    }
}
