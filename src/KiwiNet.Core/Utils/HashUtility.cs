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

        /// <summary>
        /// Semi-custom variation of the FNV-1a algorithm for producing 16-bit action hashes.
        /// </summary>
        public static ushort Fnv1a16(string input)
        {
            const uint FnvPrime = 0x1000193;

            int length = Encoding.UTF8.GetByteCount(input);
            Span<byte> data = stackalloc byte[length];
            Encoding.UTF8.GetBytes(input, data);

            uint hash = 0; // standard offset basis is 0x811C9DC5

            for (int i = 0; i < length; i++)
                hash = data[i] ^ (FnvPrime * hash);

            return (ushort)((hash ^ (hash >> 16)) & 0xFFFF);
        }
    }
}
