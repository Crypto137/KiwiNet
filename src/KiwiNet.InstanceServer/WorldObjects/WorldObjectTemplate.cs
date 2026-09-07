using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.WorldObjects.Components.Templates;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObjectTemplate : GameObjectTemplate
    {
        public PositionedComponentTemplate Positioned { get; set; }

        public WorldObjectTemplate(string filePath) : base(filePath)
        {
        }
    }
}
