using KiwiNet.Core.Math;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Network;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.WorldObjects;
using KiwiNet.InstanceServer.WorldObjects.Components;

namespace KiwiNet.InstanceServer.Commands.Implementations
{
#if DEBUG
    [CommandGroup]
    public static class CheatCommands
    {
        private static uint ItemIdCount = 1000000;

        [CommandHandler("item")]
        public static string Item(object invoker, ReadOnlySpan<string> args)
        {
            string itemShortName = args.Length > 0 ? args[0] : string.Empty;

            // Create Item
            using ResourceHandle<ItemObjectTable> itemObjectTable = ResourceManager.Get<ItemObjectTable>(GameObjectSystem.ItemObjectTableFile);
            using ResourceHandle<ItemObjectTemplate> itemTemplate = itemObjectTable.Resource.GetTemplate(itemShortName);

            if (itemTemplate == null)
                return $"'{itemShortName}' is not a valid item name.";

            ItemObject item = new();
            item.Initialize(itemTemplate);

            // Create and send WorldItem
            using ResourceHandle<WorldObjectTable> worldObjectTable = ResourceManager.Get<WorldObjectTable>(GameObjectSystem.WorldObjectTableFile);
            using ResourceHandle<WorldObjectTemplate> worldItemTemplate = worldObjectTable.Resource.GetTemplate("WorldItem");

            RemotePlayer player = (RemotePlayer)invoker;
            Vector2Int position = player.Player.Positioned.GridPosition;

            WorldObject worldItem = new() { Id = ItemIdCount++ };
            worldItem.Initialize(worldItemTemplate);
            worldItem.Positioned.SetPosition(position);
            worldItem.GetComponent<WorldItemComponent>().Item = item;

            player.SendWorldObjectAdd(worldItem);

            return string.Empty;
        }
    }
#endif
}
