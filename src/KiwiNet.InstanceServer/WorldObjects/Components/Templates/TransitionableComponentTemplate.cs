using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class TransitionableComponentTemplate : ComponentTemplate<TransitionableComponent>
    {
        public TransitionableComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class TransitionableComponentTemplateFactory : ComponentTemplateFactory
    {
        public TransitionableComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new TransitionableComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Transitionable";
        }
    }
}
