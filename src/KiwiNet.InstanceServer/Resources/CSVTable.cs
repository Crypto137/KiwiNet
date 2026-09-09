using KiwiNet.Core.Logging;
using KiwiNet.Core.Utils;
using System.Text;

namespace KiwiNet.InstanceServer.Resources
{
    public class CSVTable : IResource
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public List<string> Entries { get; } = new();
        public int NumColumns { get; private set; }
        public int NumRows { get; private set; }
        public Dictionary<string, int> ColumnIndex { get; private set; } = new(StringComparer.Ordinal);
        public Dictionary<string, int> RowIndex { get; private set; } = new(StringComparer.Ordinal);

        public CSVTable() { }

        public void Load(string fileName)
        {
            Logger.Info($"Loading CSV: {fileName}");
            
            LoadFile(fileName);

            NumRows = Entries.Count / NumColumns;

            // Build column index
            for (int i = 0; i < NumColumns; i++)
            {
                string columnName = Entries[i];
                if (ColumnIndex.TryAdd(columnName, i) == false)
                    throw new ResourceException($"Duplicate column name: {columnName}");
            }

            // Build row index (if we have any data rows)
            if (NumRows > 1)
            {
                for (int i = 1; i < NumRows; i++)
                {
                    string rowName = Entries[i * NumColumns];
                    if (RowIndex.TryAdd(rowName, i) == false)
                        throw new ResourceException($"Duplicate row name: {rowName}");
                }
            }
        }

        public void Free()
        {
        }

        private void LoadFile(string fileName)
        {
            // the client gets a file from GGPK and uses std::wistream to read it here
            if (File.Exists(fileName) == false)
                throw new ResourceException("Couldn't open file.");

            using StreamReader reader = new(fileName, Encoding.Unicode);
            List<string> parsedEntries = new();

            // Header
            if (ParseUtility.GetDelimitedString(reader, '\n', out string headerRow) == false)
                throw new ResourceException("No header for table.");

            ParseRow(headerRow, parsedEntries);

            if (Entries.Count == 0)
            {
                // This is a top level table, so it can define the column structure.
                NumColumns = parsedEntries.Count;
                Entries.AddRange(parsedEntries);
            }
            else
            {
                // This is a child table, so it needs to match the structure defined by the top level parent.
                int childCount = parsedEntries.Count;

                if (childCount != NumColumns)
                    throw new ResourceException("Child table has the wrong number of columns.");

                if (childCount > 0)
                {
                    for (int i = 0; i < childCount; i++)
                    {
                        if (string.Equals(parsedEntries[i], Entries[i], StringComparison.Ordinal) == false)
                            throw new ResourceException($"{parsedEntries[i]} in child table does not match parent column: {Entries[i]}.");
                    }
                }
            }

            // Entries
            while (ParseUtility.GetDelimitedString(reader, '\n', out string row))
            {
                parsedEntries.Clear();
                ParseRow(row, parsedEntries);

                string first = parsedEntries[0];
                if (first.Length > 4 && first.EndsWith(".csv"))
                {
                    LoadFile(first);
                    continue;
                }

                if (parsedEntries.Count != NumColumns)
                    throw new ResourceException("Inconsistent number of values on a row.");

                Entries.AddRange(parsedEntries);
            }
        }

        private static void ParseRow(string row, List<string> entries)
        {
            // Rows in GGG's CSV tables are delimited by \r\n.
            // \n is removed as part of reading rows from the input stream.
            // \r is used in this function to detect the end of the row (0x20 check).
            int position = 0;
            do
            {
                bool inQuote = false;
                int subPosition = 0;

                while (true)
                {
                    if (inQuote == false && row[position] == ',')
                        break;

                    if (row[position] < 0x20)
                        break;

                    if (row[position] == '\"')
                        inQuote = inQuote == false;

                    position++;
                    subPosition++;
                }

                int start = position - subPosition;
                int length = position - start;
                if (row[start] == '\"' && row[position - 1] == '\"')
                {
                    string entry = row.Substring(start + 1, length - 2);
                    entries.Add(entry);
                }
                else
                {
                    string entry = row.Substring(start, length);
                    entries.Add(entry);
                }
            } while (row[position++] >= 0x20);
        }
    }
}
