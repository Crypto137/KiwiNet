using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Components.Items;
using KiwiNet.InstanceServer.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Commands.Implementations
{
#if DEBUG
    [CommandGroup]
    public static class DebugCommands
    {
        [CommandHandler("test")]
        public static string Test(object invoker, ReadOnlySpan<string> args)
        {
            RemotePlayer player = (RemotePlayer)invoker;

            ItemData item = new();
            item.BaseItemType = HashUtility.MurmurHash2("Metadata/Items/Weapons/OneHandWeapons/OneHandSwords/OneHandSword1");

            List<ComponentB> components =
            [
                new BaseComponent(),
                new ModsComponent(),
                new QualityComponent(),
                new SocketsComponent(),
            ];

            using MemoryStream stream = new();
            foreach (ComponentB component in components)
                component.Serialize(stream);
            item.Blob = stream.ToArray();

            InstanceClientChatMessagePacket packet = PacketFactory.Get<InstanceClientChatMessagePacket>();
            packet.Name = "Server";
            packet.Text = "item link test _";
            packet.Items.Add((packet.Text.IndexOf('_'), item));
            player.Send(packet);

            return string.Empty;
        }
    }
#endif
}
