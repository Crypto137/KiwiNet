using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ChargesComponentTemplate : ComponentTemplate<ChargesComponent>
    {
        public ChargesComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ChargesComponentTemplateFactory : ComponentTemplateFactory
    {
        public ChargesComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ChargesComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Charges";
        }
    }
}
