using KiwiNet.Core.Extensions;
using System.Buffers.Binary;

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
            TileHash = BinaryPrimitives.ReverseEndianness(stream.Read<uint>());
            DoodadHash = BinaryPrimitives.ReverseEndianness(stream.Read<uint>());
        }

        protected override void SerializeData(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
