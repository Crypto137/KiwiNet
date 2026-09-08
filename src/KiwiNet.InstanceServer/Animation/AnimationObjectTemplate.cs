using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimationObjectTemplate : GameObjectTemplate
    {
        public override void Load(string fileName)
        {
            Initialize(fileName);

            if (fileName.EndsWith(".ao") == false)
                throw new ResourceException("Filename must end with .ao");

            LoadComponentTemplates(fileName[..^3], ComponentTemplateParseParams.Animation);
        }
    }
}
