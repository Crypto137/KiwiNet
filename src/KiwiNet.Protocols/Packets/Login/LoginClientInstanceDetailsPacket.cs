using KiwiNet.Protocols.Packets.Common;

namespace KiwiNet.Protocols.Packets.Login
{
    public sealed class LoginClientInstanceDetailsPacket : Packet
    {
        public uint SessionId { get; set; }                         // used to authenticate with the instance server
        public string WorldAreaId { get; set; } = string.Empty;     // id column in the WorldAreas table
        public List<InstanceDetailsEntry> Entries { get; } = new();

        public LoginClientInstanceDetailsPacket() : base(PacketId.LoginClientInstanceDetailsPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, SessionId);
            PacketIO.WriteString(stream, WorldAreaId);

            PacketIO.WriteByte(stream, (byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
