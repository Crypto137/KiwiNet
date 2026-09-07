using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldComponentTemplateFactoryCollection
    {
        public ComponentTemplateFactory ActorFactory { get; }
        public ComponentTemplateFactory AnimatedFactory { get; }
        public ComponentTemplateFactory AreaTransitionFactory { get; }
        public ComponentTemplateFactory ChestFactory { get; }
        public ComponentTemplateFactory InventoriesFactory { get; }
        public ComponentTemplateFactory LifeFactory { get; }
        public ComponentTemplateFactory ObjectMagicPropertiesFactory { get; }
        public ComponentTemplateFactory PathfindingFactory { get; }
        public ComponentTemplateFactory PlayerFactory { get; }
        public ComponentTemplateFactory PositionedFactory { get; }
        public ComponentTemplateFactory StatsFactory { get; }
        public ComponentTemplateFactory WorldItemFactory { get; }
        public ComponentTemplateFactory NPCFactory { get; }
        public ComponentTemplateFactory LimitedLifespanFactory { get; }
        public ComponentTemplateFactory TransitionableFactory { get; }
        public ComponentTemplateFactory BaseEventsFactory { get; }

        public static WorldComponentTemplateFactoryCollection Instance { get; } = new();

        private WorldComponentTemplateFactoryCollection()
        {

        }
    }
}
