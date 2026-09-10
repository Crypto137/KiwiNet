using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.WorldObjects.Components.Templates;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldCommonComponentTemplateFactoryCollection
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

        public static WorldCommonComponentTemplateFactoryCollection Instance { get; } = new();

        private WorldCommonComponentTemplateFactoryCollection()
        {
            ComponentTemplateRegistry registry = ComponentTemplateRegistry.WorldCommon;

            // TODO: load Data/ComponentActor.dat
            ActorFactory = new ActorTemplateFactory(registry);

            // TODO: load Data/ComponentAnimated.dat
            AnimatedFactory = new AnimatedTemplateFactory(registry);

            AreaTransitionFactory = new AreaTransitionTemplateFactory(registry);

            // TODO: load Data/ComponentChest.dat
            ChestFactory = new ChestTemplateFactory(registry);

            InventoriesFactory = new InventoriesTemplateFactory(registry);

            LifeFactory = new LifeTemplateFactory(registry);

            // TODO: load Data/ComponentObjectMagicProperties.dat
            ObjectMagicPropertiesFactory = new ObjectMagicPropertiesTemplateFactory(registry);

            // TODO: load Data/ComponentPathfinding.dat
            PathfindingFactory = new PathfindingTemplateFactory(registry);

            PlayerFactory = new PlayerTemplateFactory(registry);

            // TODO: load Data/ComponentPositioned.dat
            PositionedFactory = new PositionedTemplateFactory(registry);

            // TODO: load Data/ComponentStats.dat
            StatsFactory = new StatsTemplateFactory(registry);

            WorldItemFactory = new WorldItemTemplateFactory(registry);

            // TODO: load Data/NPCs.dat
            NPCFactory = new NPCTemplateFactory(registry);

            LimitedLifespanFactory = new LimitedLifespanTemplateFactory(registry);

            TransitionableFactory = new TransitionableTemplateFactory(registry);

            BaseEventsFactory = new BaseEventsTemplateFactory(registry);
        }
    }
}
