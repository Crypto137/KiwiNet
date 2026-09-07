using KiwiNet.InstanceServer.GameObjects.Items;

namespace KiwiNet.InstanceServer.Resources.Objects.Items
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
