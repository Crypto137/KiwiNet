using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Instance
{
    public sealed class InstanceClientInstanceInformationPacket : Packet
    {
        public uint PlayerObjectId { get; set; }                // runtime id for the player game object
        public string WorldAreaId { get; set; } = string.Empty; // id column in the WorldAreas table
        public string League { get; set; } = string.Empty;      // league name
        public uint Seed { get; set; }                          // DRLG seed
        public List<uint> ObjectTemplates { get; } = new();     // murmur2 hashes of object templates to preload

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(PlayerObjectId);
            connection.Write(WorldAreaId);
            connection.Write(League);
            connection.Write(Seed);
            connection.Write((short)ObjectTemplates.Count);
            foreach (uint objectTemplate in ObjectTemplates)
                connection.Write(objectTemplate);
        }
    }
}
