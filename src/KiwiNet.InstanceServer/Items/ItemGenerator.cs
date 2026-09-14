using KiwiNet.Core.Math;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.InstanceServer.Items.Components;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;
using KiwiNet.InstanceServer.WorldObjects.Components;

namespace KiwiNet.InstanceServer.Items
{
    public static class ItemGenerator
    {
        // just a placeholder thing for now

        public static Item Generate(string shortName)
        {
            using ResourceHandle<ItemRegistry> itemRegistry = ResourceManager.Get<ItemRegistry>(ObjectSystem.ItemRegistryFile);
            using ResourceHandle<ItemTemplate> itemTemplate = itemRegistry.Resource.GetTemplate(shortName);

            if (itemTemplate == null)
                return null;

            Item item = new();
            item.Initialize(itemTemplate);

            Stack stack = item.GetComponent<Stack>();
            if (stack != null)
                stack.Quantity = 1;

            Charges charges = item.GetComponent<Charges>();
            if (charges != null)
                charges.Count = 30; // this is a hack to initialize flask charges

            return item;
        }

        public static WorldObject DropItem(Item item, Area area, Vector2Int position)
        {
            // TODO: find a more appropriate place for this
            using ResourceHandle<WorldObjectRegistry> worldObjectTable = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            using ResourceHandle<WorldObjectTemplate> worldItemTemplate = worldObjectTable.Resource.GetTemplate("WorldItem");

            WorldObject worldItem = new();
            worldItem.Initialize(worldItemTemplate, area);
            worldItem.Positioned.SetPosition(position);
            worldItem.GetComponent<WorldItem>().Item = item;

            worldItem.Wake();

            return worldItem;
        }
    }
}
