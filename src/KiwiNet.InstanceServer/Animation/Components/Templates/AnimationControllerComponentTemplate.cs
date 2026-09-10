using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AnimationControllerComponentTemplate : ComponentTemplate<AnimationControllerComponent>
    {
        public AnimationControllerComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AnimationControllerComponentTemplateFactory : ComponentTemplateFactory
    {
        public AnimationControllerComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AnimationControllerComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "AnimationController";
        }
    }
}
