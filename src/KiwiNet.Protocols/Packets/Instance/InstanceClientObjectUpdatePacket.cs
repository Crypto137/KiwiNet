namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectUpdate : Packet
    {
        public InstanceClientObjectUpdate() : base((PacketId)101)
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
