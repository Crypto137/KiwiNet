using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Items;
using KiwiNet.InstanceServer.GameObjects.World;
using KiwiNet.InstanceServer.Network;

namespace KiwiNet.InstanceServer.Commands.Implementations
{
#if DEBUG
    [CommandGroup]
    public static class DebugCommands
    {
        private static uint ItemIdCount = 1000000;

        [CommandHandler("test")]
        public static string Test(object invoker, ReadOnlySpan<string> args)
        {
            RemotePlayer player = (RemotePlayer)invoker;

            WorldObject worldItem = new();

            GameObjectSettings worldSettings = new()
            {
                Template = HashUtility.MurmurHash2("Metadata/MiscellaneousObjects/WorldItem"),
                Id = ItemIdCount++,
                GridPosition = player.Player.GetComponent<PositionedComponent>().GridPosition,
            };

            worldItem.Initialize(ref worldSettings);
            worldItem.GetOrCreateComponent<AnimatedComponent>();
            WorldItemComponent worldItemComponent = worldItem.GetOrCreateComponent<WorldItemComponent>();

            worldItemComponent.Item = new();
            GameObjectSettings itemSettings = new()
            {
                Template = HashUtility.MurmurHash2("Metadata/Items/Weapons/OneHandWeapons/OneHandSwords/OneHandSword1"),
            };

            worldItemComponent.Item.Initialize(ref itemSettings);

            worldItemComponent.Item.GetOrCreateComponent<BaseComponent>();
            worldItemComponent.Item.GetOrCreateComponent<ModsComponent>();
            worldItemComponent.Item.GetOrCreateComponent<QualityComponent>();
            worldItemComponent.Item.GetOrCreateComponent<SocketsComponent>();

            player.SendWorldObjectAdd(worldItem);

            return string.Empty;
        }
    }
#endif
}
