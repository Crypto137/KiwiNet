using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AreaTransitionComponentTemplate : ComponentTemplate<AreaTransitionComponent>
    {
        public AreaTransitionComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AreaTransitionComponentTemplateFactory : ComponentTemplateFactory
    {
        public AreaTransitionComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AreaTransitionComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "AreaTransition";
        }
    }
}
