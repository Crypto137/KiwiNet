using KiwiNet.Core.Math;
using KiwiNet.Protocols;
using System.Numerics;

namespace KiwiNet.InstanceServer.GameObjects.Components
{
    public sealed class PositionedComponent : Component
    {
        public const float WorldUnitsPerGridCell = 1000f / 92f; // 0x412DE9BD or 10.869565

        public Vector2Int GridPosition { get; private set; }
        public Vector2 WorldPosition { get; private set; }

        public override void Serialize(Stream stream)
        {
            byte flags = 0;

            PacketIO.WriteInt32(stream, GridPosition.X);
            PacketIO.WriteInt32(stream, GridPosition.Y);
            PacketIO.WriteFloat(stream, 0);    // rotation? normalized to pi*2, need to confirm when we get a character showing in-game
            PacketIO.WriteByte(stream, flags);
            PacketIO.WriteFloat(stream, 0);

            if ((flags & 0x4) != 0)
            {
                PacketIO.WriteFloat(stream, 0);
                PacketIO.WriteUInt32(stream, 0);
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
