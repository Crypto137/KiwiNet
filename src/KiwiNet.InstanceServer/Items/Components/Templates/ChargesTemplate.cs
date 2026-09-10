using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class ChargesTemplate : ComponentTemplate<Charges>
    {
        public ChargesTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class ChargesTemplateFactory : ComponentTemplateFactory
    {
        public ChargesTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new ChargesTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Charges);
        }
    }
}
