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

        protected override void DeserializeData(Stream stream)
        {
            TileHash = PacketIO.ReadUInt32(stream);
            DoodadHash = PacketIO.ReadUInt32(stream);
        }
    }
}
