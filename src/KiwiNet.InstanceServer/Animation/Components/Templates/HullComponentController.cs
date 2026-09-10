using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class HullComponentTemplate : ComponentTemplate<HullComponent>
    {
        public HullComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class HullComponentTemplateFactory : ComponentTemplateFactory
    {
        public HullComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new HullComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Hull";
        }
    }
}
