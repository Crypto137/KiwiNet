using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class BaseEventsComponentTemplate : ComponentTemplate<BaseEventsComponent>
    {
        public BaseEventsComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class BaseEventsComponentTemplateFactory : ComponentTemplateFactory
    {
        public BaseEventsComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new BaseEventsComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "BaseEvents";
        }
    }
}
