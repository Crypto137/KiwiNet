using KiwiNet.InstanceServer.Objects;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Animation
{
    public sealed class AnimatedObject : ObjectBase<AnimatedObjectTemplate>
    {
        public void Initialize(ResourceHandle<AnimatedObjectTemplate> templateHandle)
        {
            InitializeComponents(templateHandle);

            foreach (Component component in _components)
                component.PostInitialize();
        }
    }
}
