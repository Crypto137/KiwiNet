using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Animation.Components.Templates
{
    public sealed class AttachedAnimatedObjectComponentTemplate : ComponentTemplate<AttachedAnimatedObjectComponent>
    {
        public AttachedAnimatedObjectComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AttachedAnimatedObjectComponentTemplateFactory : ComponentTemplateFactory
    {
        public AttachedAnimatedObjectComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

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
