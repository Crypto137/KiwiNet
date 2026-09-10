using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AnimationControllerTemplate : ComponentTemplate<AnimationController>
    {
        public AnimationControllerTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class AnimationControllerTemplateFactory : ComponentTemplateFactory
    {
        public AnimationControllerTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new AnimationControllerTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(AnimationController);
        }
    }
}
