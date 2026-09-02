using KiwiNet.Core.Network;

namespace KiwiNet.Protocols
{
    public abstract class Packet
    {
        public PacketId Id { get; }

        public Packet(PacketId id)
        {
            Id = id;
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
