using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QuestTemplate : ComponentTemplate<Quest>
    {
        public QuestTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class QuestTemplateFactory : ComponentTemplateFactory
    {
        public QuestTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new QuestTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Quest);
        }
    }
}
