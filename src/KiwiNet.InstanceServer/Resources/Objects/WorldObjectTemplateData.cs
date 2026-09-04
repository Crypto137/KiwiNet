namespace KiwiNet.InstanceServer.Resources.Objects
{
    public sealed class WorldObjectTemplateData : IResourceData
    {
        public WorldObjectTemplate Template { get; private set; }

        public void Load(string filePath)
        {
            Template = new(filePath);

            // Positioned is always instantiated first before 
            Template.Positioned = new()
            {
                Field0 = 0,
                Field1 = false,
                Field2 = false,
                Field3 = 1f,
            };

            Template.AddComponent(Template.Positioned, "Positioned");
        }

        public void Free()
        {
        }
    }
}
