using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimationObjectTemplateResource : IResource
    {
        public GameObjectTemplate Template { get; private set; }

        public void Load(string fileName)
        {
            Template = new(fileName);

            if (fileName.EndsWith(".ao") == false)
                throw new ResourceException("Filename must end with .ao");

            Template.LoadComponentTemplates(fileName[..^3], ComponentTemplateParseParams.Animation);
        }

        public void Free()
        {
        }
    }
}
