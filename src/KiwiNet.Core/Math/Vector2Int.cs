namespace KiwiNet.Core.Math
{
    public struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int()
        {
        }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2Int(int size)
        {
            X = size;
            Y = size;
        }
    }
}
