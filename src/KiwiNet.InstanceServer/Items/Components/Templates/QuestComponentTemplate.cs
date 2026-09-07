using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class QuestComponentTemplate : ComponentTemplate<QuestComponent>
    {
        public QuestComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class QuestComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new QuestComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Quest";
        }
    }
}
