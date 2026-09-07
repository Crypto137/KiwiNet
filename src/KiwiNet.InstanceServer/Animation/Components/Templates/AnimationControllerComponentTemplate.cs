using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AnimationControllerComponentTemplate : ComponentTemplate<AnimationControllerComponent>
    {
        public AnimationControllerComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AnimationControllerComponentTemplateFactory : ComponentTemplateFactory
    {
        public AnimationControllerComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AnimationControllerComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "AnimationController";
        }
    }
}
