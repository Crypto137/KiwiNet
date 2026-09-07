using KiwiNet.InstanceServer.GameObjects.Animation;

namespace KiwiNet.InstanceServer.Resources.Objects.Animation
{
    public sealed class AnimationControllerComponentTemplate : ComponentTemplate<AnimationControllerComponent>
    {
        public AnimationControllerComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AnimationControllerComponentTemplateFactory : ComponentTemplateFactory
    {
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
