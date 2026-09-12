using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public enum ScreenType
    { 
        Waypoint,
        Stash,
        Type2,
        Type3,
        Altar,
    }

    public class InstanceClientOpenScreen : Packet
    {
        public ScreenType Screen { get; set; }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write((int)Screen);
        }
    }
}
