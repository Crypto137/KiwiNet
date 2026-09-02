using KiwiNet.Core.Network;

namespace KiwiNet.Protocols
{
    /// <summary>
    /// Deserializes <see cref="Packet"/> instances from <see cref="NetworkConnection"/> and passes them to <see cref="IPacketHandler"/>.
    /// </summary>
    public abstract class PacketSerializer
    {
        private readonly IPacketHandler _packetHandler;

        public PacketSerializer(IPacketHandler packetHandler)
        {
            _packetHandler = packetHandler;
        }

        /// <summary>
        /// Deserializes and routes all available packets from the provided <see cref="NetworkConnection"/>.
        /// </summary>
        public static bool DeserializeAllPackets(NetworkConnection connection, List<PacketSerializer> serializers)
        {
            bool result;

            do
            {
                result = DeserializePacket(connection, serializers);
            }
            while (result);

            if (connection.IsConnected == false)
            {
                foreach (PacketSerializer serializer in serializers)
                    serializer.TryDeserializePacket(PacketId.DisconnectionId, connection);
            }

            return result;
        }

        /// <summary>
        /// Tries to deserialize a packet from a <see cref="NetworkConnection"/> using the provided <see cref="PacketSerializer"/> list.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if any of the serializers was able to deserialize the packet.
        /// </returns>
        public static bool DeserializePacket(NetworkConnection connection, List<PacketSerializer> serializers)
        {
            PacketId packetId = connection.Read<PacketId>();
            if (connection.IsTruncated)
            {
                connection.CancelRead();
                return false;
            }

            bool success = false;

            foreach (PacketSerializer serializer in serializers)
            {
                if (serializer.TryDeserializePacket(packetId, connection))
                {
                    success = true;
                    break;
                }
            }

            if (success == false)
                throw new Exception($"Unable to deserialize packet with pid {packetId} ({(int)packetId})");

            if (connection.IsTruncated)
            {
                if (connection.GetAvailableReceiveCapacity() == 0)
                    throw new Exception("Received a packet that is too large to deserialize");

                connection.CancelRead();
                return false;
            }

            connection.ConfirmRead();
            return true;
        }

        /// <summary>
        /// Deserializes a <see cref="Packet"/> instance from a <see cref="NetworkConnection"/> and routes it to the bound <see cref="IPacketHandler"/>.
        /// </summary>
        /// <remarks>
        /// This method is overriden in the client to handle game object replication, but all other serializers use the default implementation.
        /// </remarks>
        public virtual bool TryDeserializePacket(PacketId packetId, NetworkConnection connection)
        {
            if (packetId == PacketId.DisconnectionId)
            {
                _packetHandler.HandlePacket(connection, null);
                return true;
            }

            // null return is valid here when this particular serializer can't handle this packet id.
            Packet packet = ConstructAndDeserializePacket(packetId, connection);
            if (packet == null)
                return false;

            if (connection.IsTruncated == false)
                _packetHandler.HandlePacket(connection, packet);

            // TODO: return to the pool?
            // packet.Destroy();

            return true;
        }

        /// <summary>
        /// Constructs a <see cref="Packet"/> instance for the specified <see cref="PacketId"/> and deserializes it from the provided <see cref="NetworkConnection"/>.
        /// </summary>
        /// <returns><see cref="Packet"/> for parsed packets, <see langword="null"/> for unknown packets.</returns>
        protected abstract Packet ConstructAndDeserializePacket(PacketId packetId, NetworkConnection connection);
    }
}
