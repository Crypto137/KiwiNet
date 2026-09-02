using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Patching
{
    public sealed class PatchingProtocolVersion : Packet
    {
        public byte Field0 { get; set; }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
        }
    }
}
