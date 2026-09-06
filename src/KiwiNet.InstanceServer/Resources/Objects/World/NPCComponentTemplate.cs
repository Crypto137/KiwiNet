using KiwiNet.InstanceServer.GameObjects.World;

namespace KiwiNet.InstanceServer.Resources.Objects.World
{
    public sealed class NPCComponentTemplate : ComponentTemplate<NPCComponent>
    {
        public NPCComponentTemplate(GameObjectTemplate gameObjectTemplate) : base(gameObjectTemplate)
        {
        }
    }

    public sealed class NPCComponentTemplateFactory : ComponentTemplateFactory
    {
        public override ComponentTemplate Allocate(GameObjectTemplate gameObjectTemplate)
        {
            return new NPCComponentTemplate(gameObjectTemplate);
        }

        public override string GetName()
        {
            return "NPC";
        }
    }
}
