using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class HullTemplate : ComponentTemplate<Hull>
    {
        public HullTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class HullTemplateFactory : ComponentTemplateFactory
    {
        public HullTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new HullTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Hull);
        }
    }
}
