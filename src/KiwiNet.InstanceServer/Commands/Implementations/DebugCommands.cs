using KiwiNet.Core.Network;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Components.Items;
using KiwiNet.InstanceServer.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Instance;

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

            ItemData item = new(HashUtility.MurmurHash2("Metadata/Items/Weapons/OneHandWeapons/OneHandSwords/OneHandSword1"));
            item.GetOrCreateComponent<BaseComponent>();
            item.GetOrCreateComponent<ModsComponent>();
            item.GetOrCreateComponent<QualityComponent>();
            item.GetOrCreateComponent<SocketsComponent>();

            InstanceClientChatMessagePacket packet = PacketFactory.Get<InstanceClientChatMessagePacket>();
            packet.Id = (byte)PacketId.InstanceClientChatMessagePacketId;
            packet.Name = "Server";
            packet.Text = "item link test _";
            packet.Items.Add((packet.Text.IndexOf('_'), item));
            player.Send(packet);

            return string.Empty;
        }
    }
#endif
}
