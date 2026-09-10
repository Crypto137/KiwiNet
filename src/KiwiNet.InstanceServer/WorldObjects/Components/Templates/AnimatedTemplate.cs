using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class AnimatedTemplate : ComponentTemplate<Animated>
    {
        public AnimatedTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AnimatedTemplateFactory : ComponentTemplateFactory
    {
        public AnimatedTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AnimatedTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Animated);
        }
    }
}
