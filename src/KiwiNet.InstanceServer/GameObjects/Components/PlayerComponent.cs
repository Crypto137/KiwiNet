using KiwiNet.Protocols;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class PlayerComponent : Component
    {
        public string Name { get; set; } = string.Empty;

        public override void Serialize(Stream stream)
        {
            PacketIO.WriteString(stream, Name);

            // dummy bytes
            for (int i = 0; i < 30; i++)
                PacketIO.WriteByte(stream, 0);
        }
    }
}
