using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.WorldObjects.Components
{
    public sealed class Animated : WorldComponent
    {
        public string AOFilePath { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            // Optional .ao file path, probably an override
            bool hasFilePath = AOFilePath != null;
            connection.Write(hasFilePath);
            if (hasFilePath)
                connection.Write(AOFilePath);
        }
    }
}
