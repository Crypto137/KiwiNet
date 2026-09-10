using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class BaseEventsTemplate : ComponentTemplate<BaseEvents>
    {
        public BaseEventsTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class BaseEventsTemplateFactory : ComponentTemplateFactory
    {
        public BaseEventsTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new BaseEventsTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(BaseEvents);
        }
    }
}
