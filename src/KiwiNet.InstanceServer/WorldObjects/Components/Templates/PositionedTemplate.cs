using KiwiNet.InstanceServer.Objects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PositionedTemplate : ComponentTemplate<Positioned>
    {
        public int ObjectSize { get; private set; } = 0;
        public bool Blocking { get; private set; } = false;
        public bool Static { get; private set; } = false;
        public float Scale { get; private set; } = 1f;

        public PositionedTemplate(ObjectTemplate objectTemplate) : base(objectTemplate)
        {
        }

        public override bool SetBoolVariable(string name, bool value)
        {
            if (name == "blocking")
            {
                Blocking = value;
                return true;
            }
            else if (name == "static")
            {
                Static = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool SetIntVariable(string name, int value)
        {
            if (name == "scale")
            {
                Scale = value * BitConverter.UInt32BitsToSingle(0x3C23D70A); // 0.00999, probably compiler optimization of div by 100f
                return true;
            }
            else if (name == "object_size")
            {
                ObjectSize = value - 1;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public sealed class PositionedTemplateFactory : ComponentTemplateFactory
    {
        public PositionedTemplateFactory(ComponentTemplateRegistry registry) : base(registry)
        {
        }

        public override ComponentTemplate Allocate(ObjectTemplate objectTemplate)
        {
            return new PositionedTemplate(objectTemplate);
        }

        public override string GetName()
        {
            return nameof(Positioned);
        }
    }
}
