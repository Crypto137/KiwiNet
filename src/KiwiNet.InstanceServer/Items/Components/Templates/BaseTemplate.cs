using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class BaseTemplate : ComponentTemplate<Base>
    {
        public BaseTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class BaseTemplateFactory : ComponentTemplateFactory
    {
        public BaseTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new BaseTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Base);
        }
    }
}
