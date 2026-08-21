using KiwiNet.Core.Math;

namespace KiwiNet.InstanceServer.GameObjects
{
    public struct GameObjectSettings
    {
        public uint Template;       // MurmurHash2 of a file path in GGPK
        public uint Id;             // Runtime id (not sure about this yet)
        public Vector2Int GridPosition;
    }
}
