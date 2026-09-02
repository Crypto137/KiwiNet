using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace KiwiNet.Core.Network
{
    public static class NetworkConnectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReadBool(this NetworkConnection connection)
        {
            return connection.Read<byte>() != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBool(this NetworkConnection connection, bool value)
        {
            connection.Write((byte)(value ? 1 : 0));
        }

        public static string ReadString(this NetworkConnection connection, Encoding encoding)
        {
            short length = connection.Read<short>();
            if (length == 0)
                return string.Empty;

            // The game doesn't seem to use variable length encoding such as UTF-8,
            // so we don't need to worry about our buffer being too large.
            int numBytes = encoding.GetMaxByteCount(length);
            Span<byte> buffer = stackalloc byte[numBytes];
            connection.Read(buffer);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadString(this NetworkConnection connection)
        {
            return connection.ReadString(Encoding.Unicode);
        }

        public static void WriteString(this NetworkConnection connection, string str, Encoding encoding)
        {
            int length = str.Length;
            Debug.Assert(length <= short.MaxValue);
            connection.Write((short)length);

            if (length == 0)
                return;

            int numBytes = encoding.GetByteCount(str);
            Span<byte> buffer = stackalloc byte[numBytes];
            encoding.GetBytes(str, buffer);
            connection.Write(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteString(this NetworkConnection connection, string str)
        {
            connection.WriteString(str, Encoding.Unicode);
        }
    }
}
