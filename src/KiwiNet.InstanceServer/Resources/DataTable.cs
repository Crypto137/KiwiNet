using KiwiNet.Core.Extensions;
using System.Runtime.InteropServices;

namespace KiwiNet.InstanceServer.Resources
{
    public abstract class DataTableRow
    {
        public string Id { get; protected set; }

        public override string ToString()
        {
            return Id;
        }

        public abstract void ParseFrom(Stream stream, Span<byte> dynamicData);
    }

    public abstract class DataTable<TRow> : IResource where TRow: DataTableRow, new()
    {
        private const ulong DynamicDataHeader = 0xBBBBBBBBBBBBBBBB;

        private readonly List<TRow> _rows = new();
        private readonly Dictionary<string, TRow> _rowsByKey = new();

        public int NumRows { get; private set; }

        public void Load(string fileName)
        {
            using FileStream fileStream = File.OpenRead(fileName);
            NumRows = fileStream.Read<int>();

            int rowSize = GetRowSize();
            int staticDataLength = NumRows * rowSize;
            if (fileStream.Length < sizeof(int) + staticDataLength)
                throw new ResourceException("Data file too small");

            byte[] dynamicData;
            int dynamicDataLength = (int)fileStream.Length - sizeof(int) - staticDataLength;
            if (dynamicDataLength > 0)
            {
                dynamicData = new byte[dynamicDataLength];
                fileStream.Seek(sizeof(int) + staticDataLength, SeekOrigin.Begin);
                fileStream.Read(dynamicData);
                fileStream.Seek(sizeof(int), SeekOrigin.Begin);

                if (MemoryMarshal.Read<ulong>(dynamicData) != DynamicDataHeader)
                    throw new ResourceException("The format of a game data file cannot be read, is it out of date?");
            }
            else
            {
                dynamicData = Array.Empty<byte>();
            }

            for (int i = 0; i < NumRows; i++)
            {
                TRow row = new();
                row.ParseFrom(fileStream, dynamicData);
                _rows.Add(row);
                _rowsByKey.Add(row.Id, row);
            }
        }

        public void Free()
        {
        }

        public TRow GetDataRowByKey(string key)
        {
            if (_rowsByKey.TryGetValue(key, out TRow row) == false)
                return null;

            return row;
        }

        public TRow GetDataRowByIndex(int index)
        {
            if (index < 0 || index >= _rows.Count)
                return null;

            return _rows[index];
        }

        protected abstract int GetRowSize();
    }
}
