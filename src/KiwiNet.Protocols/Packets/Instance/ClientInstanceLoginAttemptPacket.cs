using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceLoginAttemptPacket : Packet
    {
        public string CharacterName { get; set; } = string.Empty;
        public uint SessionId { get; set; }

        public ClientInstanceLoginAttemptPacket() : base(PacketId.ClientInstanceLoginAttemptPacketId)
        {
        }

        public override string ToString()
        {
            return $"CharacterName={CharacterName}, SessionId=0x{SessionId:X}";
        }

        protected override void DeserializeData(Stream stream)
        {
            CharacterName = stream.ReadNetworkUtf16String();
            SessionId = BinaryPrimitives.ReverseEndianness(stream.Read<uint>());
        }

        protected override void SerializeData(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
