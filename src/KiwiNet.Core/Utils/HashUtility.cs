using System.Text;

namespace KiwiNet.Core.Utils
{
    public static class HashUtility
    {
        /// <summary>
        /// Hashes the provided input <see cref="string"/> using the MurmurHash2 algorithm.
        /// </summary>
        public static uint MurmurHash2(string input, uint seed = 0)
        {
            // based on https://github.com/jitbit/MurmurHash.net
            const uint m = 0x5bd1e995;
            const int r = 24;

            if (string.IsNullOrEmpty(input))
                return 0;

            int length = Encoding.UTF8.GetByteCount(input);
            Span<byte> data = stackalloc byte[length];
            Encoding.UTF8.GetBytes(input, data);

            uint h = seed ^ (uint)length;
            int currentIndex = 0;
            while (length >= 4)
            {
                uint k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                length -= 4;
            }
            switch (length)
            {
                case 3:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (uint)(data[currentIndex] << 16);
                    h *= m;
                    break;
                case 2:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;
                default:
                    break;
            }

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }
    }
}
