namespace KiwiNet.Core.Math
{
    public struct RectInt
    {
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public RectInt()
        {
        }

        public RectInt(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
}
