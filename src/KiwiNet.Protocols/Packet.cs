using KiwiNet.Core.Extensions;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network.Tcp;

namespace KiwiNet.Protocols
{
    public class Packet : IPacket
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public PacketId Id { get; }

        public int SerializedSize { get => 8192; }  // TODO: calculate size based on data?

        public Packet(PacketId id)
        {
            Id = id;
        }

        public void Dispose()
        {
            // TODO: pooling?
        }

        public static Packet ParseFrom(byte[] buffer, int length)
        {
            MemoryStream input = new(buffer, 0, length);
            input.Read(out PacketId pid);

            Packet packet = PacketFactory.Get<Packet>(pid);
            if (packet == null)
            {
                Logger.Warn($"Unable to allocate packet for pid {pid}");
                return null;
            }

            packet.DeserializeData(input);
            return packet;
        }

        public int Serialize(byte[] buffer, int offset)
        {
            MemoryStream stream = new(buffer, offset, buffer.Length - offset);    // TODO: reuse MemoryStream instances to avoid allocations?

            stream.Write(Id);
            SerializeData(stream);

            return (int)stream.Position;
        }

        protected virtual void DeserializeData(Stream stream) { }

        protected virtual void SerializeData(Stream stream) { }
    }
}
