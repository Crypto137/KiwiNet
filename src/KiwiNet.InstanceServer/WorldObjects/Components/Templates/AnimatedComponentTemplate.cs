using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AnimatedComponentTemplate : ComponentTemplate<AnimatedComponent>
    {
        public AnimatedComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AnimatedComponentTemplateFactory : ComponentTemplateFactory
    {
        public AnimatedComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AnimatedComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Animated";
        }
    }
}
