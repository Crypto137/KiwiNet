using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class ClientInstanceLoginAttemptPacket : Packet
    {
        public string CharacterName { get; set; } = string.Empty;
        public uint SessionId { get; set; }

        public override string ToString()
        {
            return $"CharacterName={CharacterName}, SessionId=0x{SessionId:X}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            CharacterName = connection.ReadString();
            SessionId = connection.Read<uint>();
        }
    }
}
