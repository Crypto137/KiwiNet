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
        public static void Write(this NetworkConnection connection, bool value)
        {
            connection.Write((byte)(value ? 1 : 0));
        }

        public static string ReadString(this NetworkConnection connection)
        {
            short length = connection.Read<short>();
            if (length == 0)
                return string.Empty;

            Span<byte> buffer = stackalloc byte[length * 2];
            connection.Read(buffer);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        public static string ReadStringAscii(this NetworkConnection connection)
        {
            short length = connection.Read<short>();
            if (length == 0)
                return string.Empty;

            Span<byte> buffer = stackalloc byte[length];
            connection.Read(buffer);
            string str = Encoding.ASCII.GetString(buffer);
            return str;
        }

        public static void Write(this NetworkConnection connection, string str, Encoding encoding)
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

        public static void Write(this NetworkConnection connection, string str)
        {
            connection.Write(str, Encoding.Unicode);
        }

        public static void Write<T>(this NetworkConnection connection, T obj) where T: INetworkSerializable
        {
            obj.Serialize(connection);
        }
    }
}
