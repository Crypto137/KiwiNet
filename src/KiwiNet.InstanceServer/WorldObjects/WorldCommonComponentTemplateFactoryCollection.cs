using KiwiNet.InstanceServer.GameObjects;
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
            ActorFactory = new ActorComponentTemplateFactory(registry);

            // TODO: load Data/ComponentAnimated.dat
            AnimatedFactory = new AnimatedComponentTemplateFactory(registry);

            AreaTransitionFactory = new AreaTransitionComponentTemplateFactory(registry);

            // TODO: load Data/ComponentChest.dat
            ChestFactory = new ChestComponentTemplateFactory(registry);

            InventoriesFactory = new InventoriesComponentTemplateFactory(registry);

            LifeFactory = new LifeComponentTemplateFactory(registry);

            // TODO: load Data/ComponentObjectMagicProperties.dat
            ObjectMagicPropertiesFactory = new ObjectMagicPropertiesComponentTemplateFactory(registry);

            // TODO: load Data/ComponentPathfinding.dat
            PathfindingFactory = new PathfindingComponentTemplateFactory(registry);

            PlayerFactory = new PlayerComponentTemplateFactory(registry);

            // TODO: load Data/ComponentPositioned.dat
            PositionedFactory = new PositionedComponentTemplateFactory(registry);

            // TODO: load Data/ComponentStats.dat
            StatsFactory = new StatsComponentTemplateFactory(registry);

            WorldItemFactory = new WorldItemComponentTemplateFactory(registry);

            // TODO: load Data/NPCs.dat
            NPCFactory = new NPCComponentTemplateFactory(registry);

            LimitedLifespanFactory = new LimitedLifespanComponentTemplateFactory(registry);

            TransitionableFactory = new TransitionableComponentTemplateFactory(registry);

            BaseEventsFactory = new BaseEventsComponentTemplateFactory(registry);
        }
    }
}
