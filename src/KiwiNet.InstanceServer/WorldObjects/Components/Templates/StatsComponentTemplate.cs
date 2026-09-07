using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class StatsComponentTemplate : ComponentTemplate<StatsComponent>
    {
        public StatsComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class StatsComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new StatsComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Stats";
        }
    }
}
