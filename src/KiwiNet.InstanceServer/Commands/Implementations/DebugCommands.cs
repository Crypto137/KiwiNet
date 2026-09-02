using KiwiNet.Core.Extensions;
using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.Components.Items;
using KiwiNet.InstanceServer.Network;
using KiwiNet.Protocols;
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

            List<ComponentB> components =
            [
                new BaseComponent(),
                new ModsComponent(),
                new QualityComponent(),
                new SocketsComponent(),
            ];

            using MemoryStream stream = new();
            stream.Write(HashUtility.MurmurHash2("Metadata/Items/Weapons/OneHandWeapons/OneHandSwords/OneHandSword1"));
            foreach (ComponentB component in components)
                component.Serialize(stream);
            byte[] blob = stream.ToArray();

            InstanceClientChatMessagePacket packet = PacketFactory.Get<InstanceClientChatMessagePacket>();
            packet.Name = "Server";
            packet.Text = "item link test _";
            packet.Items.Add((packet.Text.IndexOf('_'), blob));
            player.Send(packet);

            return string.Empty;
        }
    }
#endif
}
