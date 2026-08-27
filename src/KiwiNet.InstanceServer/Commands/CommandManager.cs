using KiwiNet.InstanceServer.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Instance;
using System.Reflection;

namespace KiwiNet.InstanceServer.Commands
{
    public class CommandManager
    {
        private const char CommandPrefix = '/';

        private delegate string CommandHandler(object invoker, ReadOnlySpan<string> args);

        private readonly Dictionary<string, CommandHandler> _commands = new(StringComparer.OrdinalIgnoreCase);

        public static CommandManager Instance { get; } = new();

        private CommandManager()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsDefined(typeof(CommandGroupAttribute)) == false)
                    continue;

                foreach (MethodInfo method in type.GetMethods())
                {
                    CommandHandlerAttribute commandAttribute = method.GetCustomAttribute<CommandHandlerAttribute>();
                    if (commandAttribute == null)
                        continue;

                    CommandHandler handler = method.CreateDelegate<CommandHandler>();
                    _commands.Add($"{CommandPrefix}{commandAttribute.Command}", handler);
                }
            }
        }

        public bool TryParseCommand(RemotePlayer player, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (text.Length < 2)
                return false;

            if (text[0] != CommandPrefix)
                return false;

            string[] tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string command = tokens[0];
            Span<string> args = tokens.Length > 0 ? tokens.AsSpan(1, tokens.Length - 1) : Span<string>.Empty;

            if (_commands.TryGetValue(command, out CommandHandler handler) == false)
                return false;

            string output = handler.Invoke(player, args);

            if (string.IsNullOrWhiteSpace(output) == false)
            {
                InstanceClientChatMessagePacket outputMessage = PacketFactory.Get<InstanceClientChatMessagePacket>();
                outputMessage.Name = "KiwiNet";
                outputMessage.Text = output;
                player.Send(outputMessage);
            }

            return true;
        }
    }
}
