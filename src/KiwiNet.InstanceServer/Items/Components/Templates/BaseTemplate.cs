using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class BaseTemplate : ComponentTemplate<Base>
    {
        // TODO: populate from ot/dat files
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public string Name { get; set; } = string.Empty;    // this + 104

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
