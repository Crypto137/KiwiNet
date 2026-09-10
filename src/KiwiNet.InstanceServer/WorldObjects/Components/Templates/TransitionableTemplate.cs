using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class TransitionableTemplate : ComponentTemplate<Transitionable>
    {
        public TransitionableTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class TransitionableTemplateFactory : ComponentTemplateFactory
    {
        public TransitionableTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new TransitionableTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Transitionable);
        }
    }
}
