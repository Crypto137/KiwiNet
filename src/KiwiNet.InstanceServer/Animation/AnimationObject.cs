using KiwiNet.InstanceServer.GameObjects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimationObject : GameObject<AnimationObjectTemplate>
    {
        public override void Initialize(ResourceHandle<AnimationObjectTemplate> templateHandle)
        {
            InitializeComponents(templateHandle);

            foreach (Component component in _components)
                component.PostInitialize();
        }
    }
}
