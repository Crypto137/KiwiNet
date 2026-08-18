using KiwiNet.Core.Extensions;
using KiwiNet.Protocols.Packets.Common;
using System.Buffers.Binary;

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

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(SessionId));
            stream.WriteNetworkUtf16String(WorldAreaId);

            stream.Write((byte)Entries.Count);
            foreach (InstanceDetailsEntry entry in Entries)
                entry.Serialize(stream);
        }
    }
}
