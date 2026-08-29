namespace KiwiNet.Protocols.Packets.Common
{
    // This cannot be deserialized without getting component list from the base item type definition.
    // Probably need to use strategy/dependency injection here to handle deserialization via gameplay code.
    public struct ItemData
    {
        public uint BaseItemType { get; set; }  // murmur2 hash of BaseItemTypes table's id column value
        public Memory<byte> Blob { get; set; }  // serialized components

        public void Serialize(Stream stream)
        {
            PacketIO.WriteUInt32(stream, BaseItemType);
            stream.Write(Blob.Span);
        }
    }
}
