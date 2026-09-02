using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Patching
{
    public sealed class PatchingProtocolVersion : Packet
    {
        public byte Field0 { get; set; }

        public PatchingProtocolVersion() : base(PacketId.PatchingProtocolVersionId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.Read<byte>();
        }
    }
}
