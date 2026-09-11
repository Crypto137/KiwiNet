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
        [CommandHandler("item")]
        public static string Item(object invoker, ReadOnlySpan<string> args)
        {
            string itemShortName = args.Length > 0 ? args[0] : string.Empty;

            // Create Item
            using ResourceHandle<ItemRegistry> itemRegistry = ResourceManager.Get<ItemRegistry>(ObjectSystem.ItemRegistryFile);
            using ResourceHandle<ItemTemplate> itemTemplate = itemRegistry.Resource.GetTemplate(itemShortName);

            if (itemTemplate == null)
                return $"'{itemShortName}' is not a valid item name.";

            Item item = new();
            item.Initialize(itemTemplate);
            Stack stack = item.GetComponent<Stack>();
            if (stack != null)
                stack.Quantity = 1;

            // Create and send WorldItem
            using ResourceHandle<WorldObjectRegistry> worldObjectTable = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            using ResourceHandle<WorldObjectTemplate> worldItemTemplate = worldObjectTable.Resource.GetTemplate("WorldItem");

            RemotePlayer player = (RemotePlayer)invoker;
            Vector2Int position = player.Player.Positioned.GridPosition;

            WorldObject worldItem = new();
            worldItem.Initialize(worldItemTemplate, player.Area);
            worldItem.Positioned.SetPosition(position);
            worldItem.GetComponent<WorldItem>().Item = item;

            worldItem.Wake();

            return string.Empty;
        }

        [CommandHandler("monster")]
        public static string Monster(object invoker, ReadOnlySpan<string> args)
        {
            string shortName = args.Length > 0 ? args[0] : string.Empty;

            using ResourceHandle<WorldObjectRegistry> worldObjectRegistry = ResourceManager.Get<WorldObjectRegistry>(ObjectSystem.WorldObjectRegistryFile);
            using ResourceHandle<WorldObjectTemplate> template = worldObjectRegistry.Resource.GetTemplate(shortName);

            if (template == null)
                return $"'{shortName}' is not a valid object name.";

            if (template.FileName.Contains("Monsters") == false)
                return $"'{shortName}' is not a monster.";

            RemotePlayer player = (RemotePlayer)invoker;
            Vector2Int position = player.Player.Positioned.GridPosition;

            WorldObject monster = new();
            monster.Initialize(template, player.Area);
            monster.Positioned.SetPosition(position);
            monster.Attackable = true;

            Life life = monster.GetComponent<Life>();
            if (life != null)
                life.CurrentLife = 100;

            monster.Wake();

            return string.Empty;
        }
    }
#endif
}
