using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QuestComponentTemplate : ComponentTemplate<QuestComponent>
    {
        public QuestComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class QuestComponentTemplateFactory : ComponentTemplateFactory
    {
        public QuestComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new QuestComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Quest";
        }
    }
}
