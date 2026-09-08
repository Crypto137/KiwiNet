using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.WorldObjects.Components.Templates;

namespace KiwiNet.InstanceServer.WorldObjects
{
    public sealed class WorldObjectTemplate : GameObjectTemplate
    {
        public PositionedComponentTemplate Positioned { get; set; }
        public bool Bool68 { get; set; }
        public int Dword6C { get; set; }

        public WorldObjectTemplate(string fileName) : base(fileName)
        {
        }
    }
}
