using KiwiNet.Core.Extensions;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace KiwiNet.Protocols
{
    /// <summary>
    /// Contains functions for reading and writing serialized <see cref="Packet"/> data.
    /// </summary>
    public static class PacketIO
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReadByte(Stream stream)
        {
            return (byte)stream.ReadByte();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteByte(Stream stream, byte value)
        {
            stream.WriteByte(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReadBool(Stream stream)
        {
            return stream.ReadByte() > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBool(Stream stream, bool value)
        {
            stream.WriteByte((byte)(value ? 1 : 0));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadInt16(Stream stream)
        {
            return BinaryPrimitives.ReverseEndianness(stream.Read<short>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt16(Stream stream, short value)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt32(Stream stream)
        {
            return BinaryPrimitives.ReverseEndianness(stream.Read<int>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt32(Stream stream, int value)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt32(Stream stream)
        {
            return BinaryPrimitives.ReverseEndianness(stream.Read<uint>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt32(Stream stream, uint value)
        {
            stream.Write(BinaryPrimitives.ReverseEndianness(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadFloat(Stream stream)
        {
            uint bits = ReadUInt32(stream);
            return BitConverter.UInt32BitsToSingle(bits);
        }

        public static void WriteFloat(Stream stream, float value)
        {
            uint bits = BitConverter.SingleToUInt32Bits(value);
            WriteUInt32(stream, bits);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadString(Stream stream)
        {
            short length = ReadInt16(stream);
            if (length == 0)
                return string.Empty;

            Span<byte> buffer = stackalloc byte[length * 2];
            stream.Read(buffer);
            string str = Encoding.Unicode.GetString(buffer);
            return str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteString(Stream stream, string str)
        {
            int length = str.Length;
            Debug.Assert(length <= short.MaxValue);
            WriteInt16(stream, (short)length);

            if (length == 0)
                return;

            int numBytes = Encoding.Unicode.GetByteCount(str);
            Debug.Assert(numBytes == length * 2);
            Span<byte> buffer = stackalloc byte[numBytes];
            Encoding.Unicode.GetBytes(str, buffer);
            stream.Write(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadStringAscii(Stream stream)
        {
            short length = BinaryPrimitives.ReverseEndianness(stream.Read<short>());
            if (length == 0)
                return string.Empty;

            Span<byte> buffer = stackalloc byte[length];
            stream.Read(buffer);
            string str = Encoding.ASCII.GetString(buffer);
            return str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteStringAscii(Stream stream, string str)
        {
            int length = str.Length;
            Debug.Assert(length <= short.MaxValue);
            WriteInt16(stream, (short)length);

            if (length == 0)
                return;

            int numBytes = Encoding.ASCII.GetByteCount(str);
            Debug.Assert(numBytes == length);
            Span<byte> buffer = stackalloc byte[numBytes];
            Encoding.ASCII.GetBytes(str, buffer);
            stream.Write(buffer);
        }
    }
}
