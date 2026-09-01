using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace KiwiNet.Core.Network
{
    public static class NetworkConnectionExtensions
    {
        public static string ReadString(this NetworkConnection stream, Encoding encoding)
        {
            short length = stream.Read<short>();
            if (length == 0)
                return string.Empty;

            // The game doesn't seem to use variable length encoding such as UTF-8,
            // so we don't need to worry about our buffer being too large.
            int numBytes = encoding.GetMaxByteCount(length);
            Span<byte> buffer = stackalloc byte[numBytes];
            stream.Read(buffer);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        public static string ReadString(this NetworkConnection stream)
        {
            return stream.ReadString(Encoding.Unicode);
        }

        public static void WriteString(this NetworkConnection stream, string str, Encoding encoding)
        {
            int length = str.Length;
            Debug.Assert(length <= short.MaxValue);
            stream.Write((short)length);

            if (length == 0)
                return;

            int numBytes = encoding.GetByteCount(str);
            Span<byte> buffer = stackalloc byte[numBytes];
            encoding.GetBytes(str, buffer);
            stream.Write(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteString(this NetworkConnection stream, string str)
        {
            stream.WriteString(str, Encoding.Unicode);
        }
    }
}
