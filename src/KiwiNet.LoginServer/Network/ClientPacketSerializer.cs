using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Login;

namespace KiwiNet.LoginServer.Network
{
    public sealed class ClientPacketSerializer : PacketSerializer
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public ClientPacketSerializer(IPacketHandler packetHandler) : base(packetHandler)
        {
        }

        protected override Packet ConstructAndDeserializePacket(PacketId packetId, NetworkConnection connection)
        {
            if (ConfigManager.Get<LoginServerConfig>().LogPackets)
                Logger.Debug($" IN < {packetId}");

            Packet packet = null;

            switch (packetId)
            {
                case PacketId.ClientLoginAuthenticatePacketId:
                    packet = PacketFactory.Get<ClientLoginAuthenticatePacket>();
                    break;

                case PacketId.ClientLoginRequestPasswordChangePacketId:
                    packet = PacketFactory.Get<ClientLoginRequestPasswordChangePacket>();
                    break;

                case PacketId.ClientLoginRequestDeleteCharacterPacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientLoginChooseCharacterPacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientLoginRequestCreateCharacterPacketId:
                    packet = PacketFactory.Get<ClientLoginRequestCreateCharacterPacket>();
                    break;

                case PacketId.ClientLoginRequestLeagueListPacketId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientLoginCreateAccountPacketId:
                    packet = PacketFactory.Get<ClientLoginCreateAccountPacket>();
                    break;
            }

            packet?.Deserialize(connection);
            return packet;
        }
    }
}
