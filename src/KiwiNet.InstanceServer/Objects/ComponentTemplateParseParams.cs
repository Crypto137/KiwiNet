using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Objects
{
    public class ComponentTemplateParseParams
    {
        public string CommonFileExtension { get; set; }
        public ComponentTemplateRegistry CommonRegistry { get; set; }
        public string ServerFileExtension { get; set; }
        public ComponentTemplateRegistry ServerRegistry { get; set; }
        public ResourceHandle<CsvDataTable> ObjectTable { get; set; }

        public static ComponentTemplateParseParams Item { get; } = new();
        public static ComponentTemplateParseParams World { get; } = new();
        public static ComponentTemplateParseParams Animation { get; } = new();

        private ComponentTemplateParseParams() { }

        public string GetSuperclass(string fileName)
        {
            if (ObjectTable == null)
                return null;

            CsvDataTable table = ObjectTable.Resource;

            if (table.RowIndex.TryGetValue(fileName, out int row) == false)
                return null;

            return table.GetEntry(1, row);
        }
    }
}
