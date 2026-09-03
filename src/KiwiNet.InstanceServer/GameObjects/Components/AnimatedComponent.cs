using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class AnimatedComponent : ComponentA
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
