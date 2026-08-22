namespace KiwiNet.InstanceServer.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandGroupAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandHandlerAttribute : Attribute
    {
        public string Command { get; }

        public CommandHandlerAttribute(string command)
        {
            Command = command;
        }
    }
}
