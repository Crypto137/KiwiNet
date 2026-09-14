using KiwiNet.Core.Extensions;
using KiwiNet.InstanceServer.Items;

namespace KiwiNet.InstanceServer.Resources.Tables
{
    public sealed class BaseItemTypes_Row : DataTableRow
    {
        public const int Size = 124;

        public ItemClass ItemClass { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string IdtFile { get; private set; }
        public string DisplayName { get; private set; }
        public string ParentObject { get; private set; }
        public int Level { get; private set; }
        public string Note { get; private set; }
        public string AnimatedObject { get; private set; }
        // TODO: the rest of the columns

        public BaseItemTypes_Row()
        {
        }

        public override void ParseFrom(Stream stream, Span<byte> dynamicData)
        {
            Id = dynamicData[stream.Read<int>()..].GetUnicodeString();
            ItemClass = (ItemClass)stream.Read<int>();
            Width = stream.Read<int>();
            Height = stream.Read<int>();
            IdtFile = dynamicData[stream.Read<int>()..].GetUnicodeString();
            DisplayName = dynamicData[stream.Read<int>()..].GetUnicodeString();
            ParentObject = dynamicData[stream.Read<int>()..].GetUnicodeString();
            Level = stream.Read<int>();
            Note = dynamicData[stream.Read<int>()..].GetUnicodeString();
            AnimatedObject = dynamicData[stream.Read<int>()..].GetUnicodeString();

            for (int i = 10; i < Size / 4; i++)
            {
                // TODO
                stream.Read<int>();
            }
        }
    }

    public sealed class BaseItemTypes : DataTable<BaseItemTypes_Row>
    {
        protected override int GetRowSize()
        {
            return BaseItemTypes_Row.Size;
        }
    }
}
