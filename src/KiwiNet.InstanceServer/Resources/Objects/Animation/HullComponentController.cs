using KiwiNet.InstanceServer.GameObjects.Animation;

namespace KiwiNet.InstanceServer.Resources.Objects.Animation
{
    public sealed class HullComponentTemplate : ComponentTemplate<HullComponent>
    {
        public HullComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class HullComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new HullComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Hull";
        }
    }
}
