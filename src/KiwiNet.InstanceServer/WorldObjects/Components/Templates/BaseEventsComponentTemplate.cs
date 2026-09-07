using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class BaseEventsComponentTemplate : ComponentTemplate<BaseEventsComponent>
    {
        public BaseEventsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class BaseEventsComponentTemplateFactory : ComponentTemplateFactory
    {
        public BaseEventsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

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
