using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AreaTransitionTemplate : ComponentTemplate<AreaTransition>
    {
        public AreaTransitionTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AreaTransitionTemplateFactory : ComponentTemplateFactory
    {
        public AreaTransitionTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AreaTransitionTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(AreaTransition);
        }
    }
}
