namespace KiwiNet.Core.Network
{
    public abstract class Packet
    {
        public byte Id { get; set; }

        public Packet() { }

        public override string ToString()
        {
            return Id.ToString();
        }

        public void Dispose()
        {
            // TODO: pooling?
        }

        public virtual void Serialize(NetworkConnection connection)
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize(NetworkConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
