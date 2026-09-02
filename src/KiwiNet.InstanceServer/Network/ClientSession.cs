using KiwiNet.Core.Math;
using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Network
{
    public class ClientSession
    {
        public uint Id { get; set; }
        public string CharacterName { get; set; }
        public string WorldAreaId { get; set; }
        public Vector2Int StartPosition { get; set; }

        public NetworkConnection Connection { get; set; }
    }
}
