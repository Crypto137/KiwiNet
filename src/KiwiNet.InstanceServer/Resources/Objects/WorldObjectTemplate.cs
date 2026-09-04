using KiwiNet.InstanceServer.Resources.Objects.World;

namespace KiwiNet.InstanceServer.Resources.Objects
{
    public sealed class WorldObjectTemplate : GameObjectTemplate
    {
        public PositionedComponentTemplate Positioned { get; set; }

        public WorldObjectTemplate(string filePath) : base(filePath)
        {
        }
    }
}
