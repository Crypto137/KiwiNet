using System.Runtime.InteropServices;

namespace KiwiNet.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the specified suffix from the current string.
        /// </summary>
        public static string TrimEnd(this string str, string suffix)
        {
            if (string.IsNullOrWhiteSpace(suffix) == false && str.EndsWith(suffix))
                str = str[..^suffix.Length];

            return str;
        }

        public static string GetUnicodeString(this Span<byte> bytes)
        {
            Span<char> chars = MemoryMarshal.Cast<byte, char>(bytes);

            int nullIndex = chars.IndexOf('\0');

            if (nullIndex > 0)
                chars = chars[..nullIndex];
            else if (nullIndex == 0)
                return string.Empty;

            return chars.ToString();
        }
    }
}
