using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class InventoriesComponentTemplate : ComponentTemplate<InventoriesComponent>
    {
        public InventoriesComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class InventoriesComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new InventoriesComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Inventories";
        }
    }
}
