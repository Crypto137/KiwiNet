using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class BaseTemplate : ComponentTemplate<Base>
    {
        public int XSize { get; set; }
        public int YSize { get; set; }
        public string TypeName { get; set; }
        public string GroundItem { get; set; }
        public int BaseLevel { get; set; }
        public string FlavourText { get; set; }
        public string DescriptionText { get; set; }
        public int TemporaryWeighting { get; set; }
        public int WorldObjectSize { get; set; }
        public List<string> IdtFiles { get; } = new();

        public BaseTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override bool SetStringVariable(string name, string value)
        {
            switch (name)
            {
                case "tag":
                    // TODO: load Data/Tags.dat
                    return true;

                case "idt_file":
                    IdtFiles.Add(value);
                    return true;

                case "icon":
                    // TODO: parse
                    return true;

                case "description_text":
                    DescriptionText = value;
                    return true;

                case "flavour_text":
                    FlavourText = value;
                    return true;

                case "type_name":
                    TypeName = value;
                    return true;

                case "ground_item":
                    GroundItem = value;
                    return true;
            }

            return false;
        }

        public override bool SetIntVariable(string name, int value)
        {
            switch (name)
            {
                case "x_size":
                    XSize = value;
                    return true;

                case "y_size":
                    YSize = value;
                    return true;

                case "base_level":
                    BaseLevel = value;
                    return true;

                case "temporary_weighting":
                    TemporaryWeighting = value;
                    return true;

                case "world_object_size":
                    WorldObjectSize = value;
                    return true;
            }

            return false;
        }
    }

    public sealed class BaseTemplateFactory : ComponentTemplateFactory
    {
        public BaseTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new BaseTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Base);
        }

        public override void ApplyTableData(ComponentTemplate componentTemplate, string fileName)
        {
            // TODO: load real data from .dat
            BaseTemplate baseTemplate = (BaseTemplate)componentTemplate;
            baseTemplate.XSize = 1;
            baseTemplate.YSize = 1;
        }
    }
}
