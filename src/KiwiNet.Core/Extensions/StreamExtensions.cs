using System.Runtime.InteropServices;

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
    }
}
