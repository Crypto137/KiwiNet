using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public sealed class SkillGemComponent : ItemComponent
    {
        public uint Experience { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Experience);            
        }
    }
}
