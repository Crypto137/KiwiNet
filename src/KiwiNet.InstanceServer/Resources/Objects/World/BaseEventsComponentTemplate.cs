using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class BaseEventsComponentTemplate : ComponentTemplate<BaseEventsComponent>
    {
        public BaseEventsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class BaseEventsComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new BaseEventsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "BaseEvents";
        }
    }
}
