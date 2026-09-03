using KiwiNet.Core.Math;
using KiwiNet.Core.Network;
using System.Numerics;

namespace KiwiNet.InstanceServer.GameObjects.World
{
    [Flags]
    public enum PositionedSerializeFlags : byte
    {
        None         = 0,
        HasFlag235   = 1 << 0,
        HasFlag232   = 1 << 1,
        HasExtraData = 1 << 2,
    }

    public sealed class PositionedComponent : WorldComponent
    {
        public const float WorldUnitsPerGridCell = 1000f / 92f; // 0x412DE9BD or 10.869565

        public Vector2 WorldPosition { get; private set; }
        public Vector2Int GridPosition { get; private set; }
        public float Rotation { get; set; }     // radians in the [0, 2pi] range
        public float Scale { get; set; } = 1f;

        public float HeightOffset { get; set; } = float.MaxValue;
        public int Dword64 { get; set; } = int.MaxValue;

        public bool Flag232 { get; set; }
        public bool Flag235 { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            PositionedSerializeFlags flags = PositionedSerializeFlags.None;

            if (Flag235)
                flags |= PositionedSerializeFlags.HasFlag235;

            if (Flag232)
                flags |= PositionedSerializeFlags.HasFlag232;

            if (Dword64 != int.MaxValue)
                flags |= PositionedSerializeFlags.HasExtraData;

            connection.Write(GridPosition.X);
            connection.Write(GridPosition.Y);
            connection.Write(Rotation);
            connection.Write((byte)flags);
            connection.Write(Scale);

            if (flags.HasFlag(PositionedSerializeFlags.HasExtraData))
            {
                connection.Write(Dword64);
                connection.Write(HeightOffset);
            }

            // the client converts integer grid cell to float coordinates here
        }

        public void SetPosition(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;

            float worldX = (gridPosition.X + 0.5f) * WorldUnitsPerGridCell;
            float worldY = (gridPosition.Y + 0.5f) * WorldUnitsPerGridCell;
            WorldPosition = new(worldX, worldY);
        }
    }
}
