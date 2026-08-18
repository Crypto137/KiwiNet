using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;
using KiwiNet.Protocols.Packets.Login;

namespace KiwiNet.Protocols
{
    public class PacketFactory
    {
        // TODO: packet pooling?

        public static T Get<T>() where T: Packet, new()
        {
            return new();
        }

        public static T Get<T>(PacketId packetId) where T: Packet
        {
            Packet packet;

            switch (packetId)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    packet = new ClientInstanceLoginAttemptPacket();
                    break;

                case PacketId.InstanceClientLoginAttemptReplyPacketId:
                    packet = new InstanceClientLoginAttemptReplyPacket();
                    break;

                case PacketId.InstanceClientInstanceInformationPacketId:
                    packet = new InstanceClientInstanceInformationPacket();
                    break;

                case PacketId.InstanceClientInstanceDetailsPacketId:
                    packet = new InstanceClientInstanceDetailsPacket();
                    break;

                case PacketId.ClientInstanceTerrainGenerationResultId:
                    packet = new ClientInstanceTerrainGenerationResult();
                    break;

                case PacketId.InstanceClientServerFrameDurationPacketId:
                    packet = new InstanceClientServerFrameDurationPacket();
                    break;

                case PacketId.ClientLoginAuthenticatePacketId:
                    packet = new ClientLoginAuthenticatePacket();
                    break;

                case PacketId.LoginClientAuthenticateReplyPacketId:
                    packet = new LoginClientAuthenticateReplyPacket();
                    break;

                case PacketId.ClientLoginRequestPasswordChangePacketId:
                    packet = new ClientLoginRequestPasswordChangePacket();
                    break;

                case PacketId.ClientLoginRequestCreateCharacterPacketId:
                    packet = new ClientLoginRequestCreateCharacterPacket();
                    break;

                case PacketId.LoginClientInstanceDetailsPacketId:
                    packet = new LoginClientInstanceDetailsPacket();
                    break;

                case PacketId.LoginClientCharacterListPacketId:
                    packet = new LoginClientCharacterListPacket();
                    break;

                case PacketId.LoginClientLeagueListPacketId:
                    packet = new LoginClientLeagueListPacket();
                    break;

                case PacketId.InstanceClientHeartbeatReplyPacketId:
                case PacketId.InstanceClientYouAreDeadId:
                case PacketId.InstanceClientAreaChangeFailedPacketId:
                case PacketId.InstanceClientToggleMovieModeId:
                case PacketId.InstanceClientAdvanceFrameId:
                case PacketId.ClientLoginRequestLeagueListPacketId:
                    packet = new Packet(packetId);
                    break;

                case PacketId.InstanceClientOpenScreenId:
                case PacketId.InstanceClientPartyLeftId:
                case PacketId.InstanceClientTradeEndedId:
                    packet = new IntPacket(packetId);
                    break;

                case PacketId.InstanceClientContactRemoveId:
                case PacketId.InstanceClientAreaChangeNotificationPacketId:
                case PacketId.ClientLoginRequestDeleteCharacterPacketId:
                case PacketId.ClientLoginChooseCharacterPacketId:
                    packet = new StringPacket(packetId);
                    break;

                case PacketId.InstanceClientForcedDisconnectionWarningPacketId:
                case PacketId.LoginClientRequestPasswordChangeReplyPacketId:
                case PacketId.LoginClientRequestDeleteCharacterReplyPacketId:
                case PacketId.LoginClientChooseCharacterReplyPacketId:
                case PacketId.LoginClientRequestCreateCharacterReplyPacketId:
                case PacketId.LoginClientDisconnectPlayerPacketId:
                case PacketId.LoginClientCreateAccountResultPacketId:
                    packet = new BackendErrorPacket(packetId);
                    break;

                default:
                    packet = new Packet(packetId);
                    break;
            }

            return packet as T;
        }
    }
}
