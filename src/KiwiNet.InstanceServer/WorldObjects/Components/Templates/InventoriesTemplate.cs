using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class InventoriesTemplate : ComponentTemplate<Inventories>
    {
        public int PlayerIndex { get; private set; }
        public int StatsIndex { get; private set; }
        public int LifeIndex { get; private set; }

        public InventoriesTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Player));
            dependencies.Add(nameof(Stats));
        }

        public override void PostProcess()
        {
            if (_objectTemplate.ComponentIndicesByName.TryGetValue(nameof(Player), out int playerIndex))
                PlayerIndex = playerIndex;
            else
                PlayerIndex = -1;

            if (_objectTemplate.ComponentIndicesByName.TryGetValue(nameof(Stats), out int statsIndex))
                StatsIndex = statsIndex;
            else
                StatsIndex = -1;

            if (_objectTemplate.ComponentIndicesByName.TryGetValue(nameof(Life), out int lifeIndex))
                LifeIndex = lifeIndex;
            else
                LifeIndex = -1;
        }
    }

    public sealed class InventoriesTemplateFactory : ComponentTemplateFactory
    {
        public InventoriesTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new InventoriesTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Inventories);
        }
    }
}
