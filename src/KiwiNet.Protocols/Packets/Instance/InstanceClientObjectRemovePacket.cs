namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectRemovePacket : Packet
    {
        public InstanceClientObjectRemovePacket() : base((PacketId)102)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeData(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
