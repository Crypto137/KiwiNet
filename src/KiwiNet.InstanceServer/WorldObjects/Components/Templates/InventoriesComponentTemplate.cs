using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class InventoriesComponentTemplate : ComponentTemplate<InventoriesComponent>
    {
        public InventoriesComponentTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }
    }

    public sealed class InventoriesComponentTemplateFactory : ComponentTemplateFactory
    {
        public InventoriesComponentTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new InventoriesComponentTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return "Inventories";
        }
    }
}
