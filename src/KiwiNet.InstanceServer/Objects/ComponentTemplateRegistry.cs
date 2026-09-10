namespace KiwiNet.InstanceServer.Objects
{
    /// <summary>
    /// Contains globally accessible collections of <see cref="ComponentTemplateFactory"/>.
    /// </summary>
    public sealed class ComponentTemplateRegistry
    {
        public Dictionary<string, ComponentTemplateFactory> Factories { get; } = new(StringComparer.Ordinal);

        public static ComponentTemplateRegistry ItemCommon { get; } = new();
        public static ComponentTemplateRegistry ItemServer { get; } = new();
        public static ComponentTemplateRegistry WorldCommon { get; } = new();
        public static ComponentTemplateRegistry WorldServer { get; } = new();
        public static ComponentTemplateRegistry AnimationCommon { get; } = new();
        public static ComponentTemplateRegistry AnimationServer { get; } = new();

        private ComponentTemplateRegistry() { }
    }
}
