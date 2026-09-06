using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class AreaTransitionComponentTemplate : ComponentTemplate<AreaTransitionComponent>
    {
        public AreaTransitionComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class AreaTransitionComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new AreaTransitionComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "AreaTransition";
        }
    }
}
