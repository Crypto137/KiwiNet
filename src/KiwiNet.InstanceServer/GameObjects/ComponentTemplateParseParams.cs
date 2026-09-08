namespace KiwiNet.InstanceServer.GameObjects
{
    public class ComponentTemplateParseParams
    {
        public string CommonFileExtension { get; set; }
        public ComponentTemplateRegistry CommonRegistry { get; set; }
        public string ServerFileExtension { get; set; }
        public ComponentTemplateRegistry ServerRegistry { get; set; }

        public static ComponentTemplateParseParams Item { get; } = new();
        public static ComponentTemplateParseParams World { get; } = new();
        public static ComponentTemplateParseParams Animation { get; } = new();

        private ComponentTemplateParseParams() { }
    }
}
