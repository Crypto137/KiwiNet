using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KiwiNet.Core.Extensions
{
    public static class StreamExtensions
    {
        public static bool Read<T>(this Stream stream, out T value) where T: unmanaged
        {
            value = default;
            Span<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
            return stream.Read(bytes) == bytes.Length;
        }

        public static T Read<T>(this Stream stream) where T: unmanaged
        {
            if (stream.Read(out T value) == false)
                throw new IOException();

            return value;
        }

        public static void Write<T>(this Stream stream, T value) where T: unmanaged
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
            stream.Write(bytes);
        }

        public static string ReadNetworkAsciiString(this Stream stream)
        {
            short length = BinaryPrimitives.ReverseEndianness(stream.Read<short>());
            Span<byte> buffer = stackalloc byte[length];
            stream.Read(buffer);
            return Encoding.ASCII.GetString(buffer);
        }

        public static void WriteNetworkAsciiString(this Stream stream, string str)
        {
            int numBytes = Encoding.ASCII.GetByteCount(str);
            Span<byte> buffer = stackalloc byte[numBytes];
            Encoding.ASCII.GetBytes(str, buffer);

            short length = BinaryPrimitives.ReverseEndianness((short)numBytes);
            stream.Write(length);
            stream.Write(buffer);
        }

        public static string ReadNetworkUtf16String(this Stream stream)
        {
            if (stream.Read(out short length) == false)
                throw new Exception();

            if (length == 0)
                return string.Empty;

            length = BinaryPrimitives.ReverseEndianness(length);

            Span<byte> buffer = stackalloc byte[length * 2];
            stream.Read(buffer);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        public static void WriteNetworkUtf16String(this Stream stream, string str)
        {
            short length = (short)str.Length;
            length = BinaryPrimitives.ReverseEndianness(length);
            stream.Write(length);

            int numBytes = Encoding.Unicode.GetByteCount(str);
            Debug.Assert(numBytes == str.Length * 2);
            Span<byte> buffer = stackalloc byte[numBytes];
            Encoding.Unicode.GetBytes(str, buffer);
            stream.Write(buffer);
        }
    }
}
