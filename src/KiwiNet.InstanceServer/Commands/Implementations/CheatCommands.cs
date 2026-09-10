using KiwiNet.Core.Math;
using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Items;
using KiwiNet.InstanceServer.Items.Components;
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
        private static uint NextSpawnedId = 1000000;

        [CommandHandler("item")]
        public static string Item(object invoker, ReadOnlySpan<string> args)
        {
            string itemShortName = args.Length > 0 ? args[0] : string.Empty;

            // Create Item
            using ResourceHandle<ItemRegistry> itemObjectTable = ResourceManager.Get<ItemRegistry>(ObjectSystem.ItemRegistryFile);
            using ResourceHandle<ItemTemplate> itemTemplate = itemObjectTable.Resource.GetTemplate(itemShortName);

            if (itemTemplate == null)
                return $"'{itemShortName}' is not a valid item name.";

            Item item = new();
            item.Initialize(itemTemplate);
            StackComponent stack = item.GetComponent<StackComponent>();
            if (stack != null)
                stack.Quantity = 1;

            // Create and send WorldItem
            using ResourceHandle<WorldObjectRegistry> worldObjectTable = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            using ResourceHandle<WorldObjectTemplate> worldItemTemplate = worldObjectTable.Resource.GetTemplate("WorldItem");

            RemotePlayer player = (RemotePlayer)invoker;
            Vector2Int position = player.Player.Positioned.GridPosition;

            WorldObject worldItem = new() { Id = NextSpawnedId++ };
            worldItem.Initialize(worldItemTemplate);
            worldItem.Positioned.SetPosition(position);
            worldItem.GetComponent<WorldItemComponent>().Item = item;

            player.SendWorldObjectAdd(worldItem);

            return string.Empty;
        }

        [CommandHandler("monster")]
        public static string Monster(object invoker, ReadOnlySpan<string> args)
        {
            string shortName = args.Length > 0 ? args[0] : string.Empty;

            using ResourceHandle<WorldObjectRegistry> worldObjectTable = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            using ResourceHandle<WorldObjectTemplate> template = worldObjectTable.Resource.GetTemplate(shortName);

            if (template == null)
                return $"'{shortName}' is not a valid object name.";

            if (template.FileName.Contains("Monsters") == false)
                return $"'{shortName}' is not a monster.";

            RemotePlayer player = (RemotePlayer)invoker;
            Vector2Int position = player.Player.Positioned.GridPosition;

            WorldObject monster = new() { Id = NextSpawnedId++ };
            monster.Initialize(template);
            monster.Positioned.SetPosition(position);

            LifeComponent life = monster.GetComponent<LifeComponent>();
            if (life != null)
                life.Life = 100;

            player.SendWorldObjectAdd(monster);

            return string.Empty;
        }
    }
#endif
}
