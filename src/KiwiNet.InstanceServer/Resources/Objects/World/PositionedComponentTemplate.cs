using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class PositionedComponentTemplate : ComponentTemplate
    {
        public int Field0 { get; set; }
        public bool Field1 { get; set; }
        public bool Field2 { get; set; }
        public float Field3 { get; set; }

        public override Component CreateComponent(GameObject owner)
        {
            PositionedComponent component = new();

            component.Initialize(this, owner);

            return component;
        }
    }
}
