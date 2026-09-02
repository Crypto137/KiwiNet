using KiwiNet.Core.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class ClientLobbyPacketSerializer : PacketSerializer
    {
        public ClientLobbyPacketSerializer(IPacketHandler packetHandler) : base(packetHandler)
        {
        }

        protected override Packet ConstructAndDeserializePacket(PacketId packetId, NetworkConnection connection)
        {
            Packet packet = null;

            switch (packetId)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    packet = PacketFactory.Get<ClientInstanceLoginAttemptPacket>();
                    break;
            }

            packet?.Deserialize(connection);
            return packet;
        }
    }
}
