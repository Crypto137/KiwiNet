using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class ClientLoginCreateAccountPacket : Packet
    {
        public string Field0 { get; set; }              // email or name
        public string Field1 { get; set; }              // email or name
        public byte[] Field2 { get; } = new byte[32];   // password hash

        public ClientLoginCreateAccountPacket() : base(PacketId.ClientLoginCreateAccountPacketId)
        {
        }

        public override void Deserialize(NetworkConnection connection)
        {
            Field0 = connection.ReadString();
            Field1 = connection.ReadString();
            connection.Read(Field2);
        }
    }
}
