namespace KiwiNet.InstanceServer.GameObjects
{
    public class ComponentTemplateParseParams
    {
        public string CommonFileExtension { get; set; }
        public ComponentTemplateRegistry CommonRegistry { get; set; }
        public string ServerFileExtension { get; set; }
        public ComponentTemplateRegistry ServerRegistry { get; set; }

        public static ComponentTemplateParseParams ItemParams { get; } = new();
        public static ComponentTemplateParseParams WorldParams { get; } = new();
        public static ComponentTemplateParseParams AnimationParams { get; } = new();
    }
}
