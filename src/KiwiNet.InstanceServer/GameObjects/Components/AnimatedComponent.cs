using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class AnimatedComponent : ComponentA
    {
        public string AOFilePath { get; set; }

        public override void Serialize(Stream stream)
        {
            // Optional .ao file path, probably an override
            bool hasFilePath = AOFilePath != null;
            PacketIO.WriteBool(stream, hasFilePath);
            if (hasFilePath)
                PacketIO.WriteString(stream, AOFilePath);
        }
    }
}
