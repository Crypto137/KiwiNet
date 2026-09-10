using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimatedObjectTemplate : ObjectTemplate
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
