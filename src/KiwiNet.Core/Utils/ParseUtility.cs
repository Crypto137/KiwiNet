using System.Text;

namespace KiwiNet.Core.Utils
{
    /// <summary>
    /// Provides functionality for parsing various plain text formats from a <see cref="TextReader"/>.
    /// </summary>
    public static class ParseUtility
    {
        // TODO: optimize StringBuilder / string allocations here?

        public const char StringLiteralDelimiter = '\"';

        public static void SkipWhiteSpace(TextReader reader)
        {
            while (true)
            {
                int peek = reader.Peek();
                if (peek == -1)
                    return;

                char c = (char)peek;
                if (char.IsWhiteSpace(c) == false)
                    break;

                reader.Read();
            }
        }

        public static void GetNextToken(TextReader reader, StringBuilder sb)
        {
            SkipWhiteSpace(reader);

            int count = 0;
            while (count < int.MaxValue)
            {
                int peek = reader.Peek();
                if (peek == -1)
                    break;

                char c = (char)peek;
                if (char.IsWhiteSpace(c))
                    break;

                sb.Append(c);
                count++;
                reader.Read();
            }
        }

        public static void GetNextToken(TextReader reader, out string token)
        {
            StringBuilder sb = new();
            GetNextToken(reader, sb);
            token = sb.ToString();
        }

        public static bool GetDelimitedString(TextReader reader, char delimiter, StringBuilder sb)
        {
            int count = 0;
            while (count < int.MaxValue)
            {
                int peek = reader.Peek();
                if (peek == -1)
                    return false;

                char c = (char)peek;
                if (c == delimiter)
                {
                    reader.Read();
                    return true;
                }

                sb.Append(c);
                count++;
                reader.Read();
            }

            return false;
        }

        public static bool GetDelimitedString(TextReader reader, char delimiter, out string str)
        {
            StringBuilder sb = new();

            if (GetDelimitedString(reader, delimiter, sb))
            {
                str = sb.ToString();
                return true;
            }
            else
            {
                str = string.Empty;
                return false;
            }
        }

        public static bool ExtractStringLiteral(TextReader reader, out string str)
        {
            str = string.Empty;

            SkipWhiteSpace(reader);

            int peek = reader.Peek();

            if (peek == -1)
                return false;

            char c = (char)peek;
            if (c != StringLiteralDelimiter)
                return false;

            reader.Read();

            return GetDelimitedString(reader, StringLiteralDelimiter, out str);
        }

        public static bool MatchToken(TextReader reader, string expectedToken)
        {
            GetNextToken(reader, out string token);
            return string.Equals(token, expectedToken, StringComparison.Ordinal);
        }

        public static bool GetInt(TextReader reader, out int value)
        {
            GetNextToken(reader, out string token);
            return int.TryParse(token, out value);
        }
    }
}
