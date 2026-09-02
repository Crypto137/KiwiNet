using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class ClientInstanceTerrainGenerationResult : Packet
    {
        public uint TileHash { get; set; }
        public uint DoodadHash { get; set; }

        public ClientInstanceTerrainGenerationResult() : base(PacketId.ClientInstanceTerrainGenerationResultId)
        {
        }

        public override string ToString()
        {
            return $"TileHash={TileHash}, DoodadHash={DoodadHash}";
        }

        public override void Deserialize(NetworkConnection connection)
        {
            TileHash = connection.Read<uint>();
            DoodadHash = connection.Read<uint>();
        }
    }
}
