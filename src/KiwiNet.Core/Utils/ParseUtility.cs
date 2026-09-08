using System.Text;

namespace KiwiNet.Core.Utils
{
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

        public static void GetNextToken(TextReader reader, out string token)
        {
            StringBuilder sb = new();

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

            token = sb.ToString();
        }

        public static bool GetDelimitedString(TextReader reader, char delimiter, out string str)
        {
            str = string.Empty;
            StringBuilder sb = new();

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
                    str = sb.ToString();
                    return true;
                }

                sb.Append(c);
                count++;
                reader.Read();
            }

            return false;
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
