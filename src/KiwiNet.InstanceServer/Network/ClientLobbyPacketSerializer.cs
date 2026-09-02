using KiwiNet.Core.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class ClientLobbyPacketSerializer : PacketSerializer
    {
        public ClientLobbyPacketSerializer(IPacketHandler packetHandler) : base(packetHandler)
        {
        }

        protected override Packet ConstructAndDeserializePacket(byte packetId, NetworkConnection connection)
        {
            Packet packet = null;

            switch ((PacketId)packetId)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    packet = PacketFactory.Get<ClientInstanceLoginAttemptPacket>();
                    packet.Id = (byte)PacketId.ClientInstanceLoginAttemptPacketId;
                    packet.Deserialize(connection);
                    break;
            }

            return packet;
        }
    }
}
