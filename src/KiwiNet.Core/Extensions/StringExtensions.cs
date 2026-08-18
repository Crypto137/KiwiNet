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
    }
}
