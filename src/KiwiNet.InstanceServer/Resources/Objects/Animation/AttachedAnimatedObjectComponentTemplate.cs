using KiwiNet.InstanceServer.GameObjects.Animation;

namespace KiwiNet.InstanceServer.Resources.Objects.Animation
{
    public sealed class AttachedAnimatedObjectComponentTemplate : ComponentTemplate<AttachedAnimatedObjectComponent>
    {
        public AttachedAnimatedObjectComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AttachedAnimatedObjectComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AttachedAnimatedObjectComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "AttachedAnimatedObject";
        }
    }
}
