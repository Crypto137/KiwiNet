using KiwiNet.Core.Math;

namespace KiwiNet.InstanceServer.Entities
{
    public struct EntitySettings
    {
        public uint Template;       // MurmurHash2 of a file path in GGPK
        public uint Id;             // Runtime id (not sure about this yet)
        public Vector2Int GridPosition;
    }
}
