using KiwiNet.InstanceServer.GameObjects;

namespace KiwiNet.InstanceServer.WorldObjects.Components.Templates
{
    public sealed class PositionedComponentTemplate : ComponentTemplate<PositionedComponent>
    {
        public int ObjectSize { get; private set; } = 0;
        public bool Blocking { get; private set; } = false;
        public bool Static { get; private set; } = false;
        public float Scale { get; private set; } = 1f;

        public PositionedComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
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

    public sealed class PositionedComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new PositionedComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "Positioned";
        }
    }
}
