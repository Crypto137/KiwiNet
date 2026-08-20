using KiwiNet.Core.Extensions;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network.Tcp;
using KiwiNet.Core.Utils;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public class InstanceClient : TcpClient
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public InstanceClient()
        {
        }

        public override void OnDataReceived(byte[] buffer, int length)
        {
            //Logger.Debug($"OnDataReceived(): {Convert.ToHexString(buffer.AsSpan(0, length))}");

            List<Packet> packets = new();   // todo: pool this
            Packet.ParseFrom(buffer, length, packets);

            foreach (Packet packet in packets)
            {
                Logger.Trace($" IN < {packet.Id}");
                ReceivePacket(packet);
            }
        }

        public void Send(Packet packet)
        {
            Logger.Trace($"OUT > {packet.Id}");
            Connection.Send(packet);
        }

        #region Handlers

        private void ReceivePacket(Packet packet)
        {
            switch (packet.Id)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    OnLoginAttempt(packet);
                    break;

                case PacketId.ClientInstanceChatMessagePacketId:
                    OnChatMessage(packet);
                    break;

                case PacketId.ClientInstanceHeartbeatPacketId:
                    OnHeartbeat();
                    break;

                case PacketId.ClientInstanceAllocatePassiveSkillPointPacketId:
                    OnAllocatePassiveSkillPoint(packet);
                    break;

                case PacketId.ClientInstanceChangeBoundSkillId:
                    OnChangeBoundSkill(packet);
                    break;

                case PacketId.ClientInstanceTerrainGenerationResultId:
                    OnTerrainGenerationResult(packet);
                    break;

                default:
                    Logger.Warn($"Unhandled packet [{(int)packet.Id}] {packet.Id}");
                    break;
            }
        }

        private void OnLoginAttempt(Packet packet)
        {
            if (packet is not ClientInstanceLoginAttemptPacket loginAttempt)
            {
                Logger.Warn("OnLoginAttempt(): Invalid packet");
                return;
            }

            Logger.Debug($"OnLoginAttempt(): {loginAttempt}");

            var reply = PacketFactory.Get<InstanceClientLoginAttemptReplyPacket>();
            reply.Field0 = 1;
            reply.Field1 = "";
            Send(reply);

            var instanceInfo = PacketFactory.Get<InstanceClientInstanceInformationPacket>();
            instanceInfo.Field0 = 1;
            instanceInfo.WorldAreaId = "1_1_1";
            instanceInfo.League = "Default";
            instanceInfo.Seed = 666;
            Send(instanceInfo);
        }

        private void OnChatMessage(Packet packet)
        {
            if (packet is not ClientInstanceChatMessagePacket chatMessage)
            {
                Logger.Warn("OnChatMessage(): Invalid packet");
                return;
            }

            Logger.Debug($"OnChatMessage(): {chatMessage.Text}");

            InstanceClientChatMessagePacket reply = PacketFactory.Get<InstanceClientChatMessagePacket>();
            reply.Name = "Player";
            reply.Text = chatMessage.Text;
            Send(reply);
        }

        private void OnHeartbeat()
        {
            Send(PacketFactory.Get<Packet>(PacketId.InstanceClientHeartbeatReplyPacketId));
        }

        private void OnAllocatePassiveSkillPoint(Packet packet)
        {
            if (packet is not IntPacket allocatePassiveSkillPoint)
            {
                Logger.Warn("OnAllocatePassiveSkillPoint(): Invalid packet");
                return;
            }

            Logger.Debug($"OnAllocatePassiveSkillPoint(): 0x{allocatePassiveSkillPoint.Value:X8}");
        }

        private void OnChangeBoundSkill(Packet packet)
        {
            if (packet is not ClientInstanceChangeBoundSkill changeBoundSkill)
            {
                Logger.Warn("OnChangeBoundSkill(): Invalid packet");
                return;
            }

            Logger.Debug($"OnChangeBoundSkill(): {changeBoundSkill}");
        }

        private void OnTerrainGenerationResult(Packet packet)
        {
            if (packet is not ClientInstanceTerrainGenerationResult terrainGenerationResult)
            {
                Logger.Warn("OnTerrainGenerationResult(): Invalid packet");
                return;
            }

            Logger.Debug($"OnTerrainGenerationResult(): {terrainGenerationResult}");
            // this is where the server disconnects the client if the hashes don't match
            // InstanceClientForcedDisconnectionWarningPacketId -> BackendError.TerrainGenerationOutOfSync

            var objAdd = PacketFactory.Get<InstanceClientObjectAddPacket>();
            objAdd.ObjectTemplate = HashUtility.MurmurHash2("Metadata/Characters/Str/Str");
            objAdd.Field1 = 0x1;

            using MemoryStream ms = new();

            ms.Write(0);        // u32  x coord?   this + 48
            ms.Write(0);        // u32  y coord?   this + 52
            ms.Write(0);        // u32  rotation?  this + 56
            ms.Write((byte)0);  // u8   flags?
            ms.Write(0);        // u32;            this + 16

            // if flags & 4 -> read u32 x2

            // other components
            for (int i = 0; i < 209; i++)
                ms.Write((byte)0);

            objAdd.Blob = ms.ToArray();

            Send(objAdd);
        }

        #endregion
    }
}
