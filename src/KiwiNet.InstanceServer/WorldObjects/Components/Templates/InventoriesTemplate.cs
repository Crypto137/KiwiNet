using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class InventoriesTemplate : ComponentTemplate<Inventories>
    {
        public InventoriesTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override void GetDependencies(List<string> dependencies)
        {
            dependencies.Add(nameof(Player));
            dependencies.Add(nameof(Stats));
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
