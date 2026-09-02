using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Patching
{
    public sealed class PatchingProtocolStart : Packet
    {
        public byte[] Field0 { get; } = new byte[32];
        public string Field1 { get; }

        public PatchingProtocolStart() : base(PacketId.PatchingProtocolStartId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
        }
    }
}
