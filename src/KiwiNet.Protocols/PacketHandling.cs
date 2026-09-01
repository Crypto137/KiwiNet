using KiwiNet.Core.Network;

namespace KiwiNet.Protocols
{
    public static class PacketHandling
    {
        public static bool DeserializePackets(BufferedNetworkStream stream, List<IPacketListener> listeners)
        {
            bool result;

            do
            {
                result = DeserializePacket(stream, listeners);
            }
            while (result);

            if (stream.IsConnected == false)
            {
                foreach (IPacketListener listener in listeners)
                    listener.ReceivePacket(PacketId.DisconnectionId, stream);
            }

            return result;
        }

        public static bool DeserializePacket(BufferedNetworkStream stream, List<IPacketListener> listeners)
        {
            PacketId packetId = stream.Read<PacketId>();
            if (stream.IsTruncated)
            {
                stream.CancelRead();
                return false;
            }

            bool success = false;

            foreach (IPacketListener listener in listeners)
            {
                if (listener.ReceivePacket(packetId, stream))
                {
                    success = true;
                    break;
                }
            }

            if (success == false)
                throw new Exception($"Unable to deserialize packet with pid {packetId} ({(int)packetId})");

            if (stream.IsTruncated)
            {
                if (stream.GetAvailableReadCapacity() == 0)
                    throw new Exception("Received a packet that is too large to deserialize");

                stream.CancelRead();
                return false;
            }

            stream.ConfirmRead();
            return true;
        }
    }
}
