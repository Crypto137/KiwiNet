using KiwiNet.Core.Network;
using KiwiNet.Protocols.Common;

namespace KiwiNet.Protocols.Login
{
    public sealed class LoginClientInstanceDetailsPacket : Packet
    {
        public uint SessionId { get; set; }                         // used to authenticate with the instance server
        public string WorldAreaId { get; set; } = string.Empty;     // id column in the WorldAreas table
        public List<InstanceDetailsEntry> Entries { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(SessionId);
            connection.Write(WorldAreaId);
            connection.Write((byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
