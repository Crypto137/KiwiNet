using KiwiNet.Core.Extensions;

namespace KiwiNet.InstanceServer.Resources.Tables
{
    public sealed class WorldAreas_Row : DataTableRow
    {
        public const int Size = 123;

        public override void ParseFrom(Stream stream, Span<byte> dynamicData)
        {
            Id = dynamicData[stream.Read<int>()..].GetUnicodeString();

            // TODO
            stream.Seek(Size - sizeof(int), SeekOrigin.Current);
        }
    }

    public class WorldAreas : DataTable<WorldAreas_Row>
    {
        protected override int GetRowSize()
        {
            return WorldAreas_Row.Size;
        }
    }
}
