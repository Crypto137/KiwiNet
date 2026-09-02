using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Common
{
    public struct ItemData
    {
        public uint BaseItemType { get; set; }  // murmur2 hash of BaseItemTypes table's id column value
        public Memory<byte> Blob { get; set; }  // serialized components

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(BaseItemType);
            connection.Write(Blob.Span);
        }

        public void Deserialize(NetworkConnection connection)
        {
            // This cannot be deserialized without getting component list from the base item type definition.
            // Probably need to use strategy/dependency injection here to handle deserialization via gameplay code.
            throw new NotImplementedException();
        }
    }
}
