using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;
using KiwiNet.InstanceServer.Resources.Tables;

namespace KiwiNet.InstanceServer.Items.Components.Templates
{
    public sealed class BaseTemplate : ComponentTemplate<Base>
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public string TypeName { get; private set; }
        public string GroundItem { get; private set; }
        public int BaseLevel { get; private set; }
        public string FlavourText { get; private set; }
        public string DescriptionText { get; private set; }
        public int TemporaryWeighting { get; private set; }
        public int WorldObjectSize { get; private set; }
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

        public void ApplyTableData(BaseItemTypes_Row row)
        {
            XSize = row.Width;
            YSize = row.Height;
            BaseLevel = row.Level;
            //TemporaryWeighting = row.TemporaryWeighting;  // TODO
            //WorldObjectSize = row.WorldObjectSize;        // TODO
            TypeName = row.DisplayName;
            IdtFiles.Add(row.IdtFile);
            GroundItem = row.GroundItem;
            FlavourText = row.FlavourText;
        }
    }

    public sealed class BaseTemplateFactory : ComponentTemplateFactory
    {
        public ResourceHandle<BaseItemTypes> BaseItemTypes { get; set; }

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
            BaseItemTypes_Row row = BaseItemTypes?.Resource.GetDataRowByKey(fileName);
            if (row == null)
                return;

            BaseTemplate baseTemplate = (BaseTemplate)componentTemplate;
            baseTemplate.ApplyTableData(row);
        }
    }
}
