using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network.Tcp;
using KiwiNet.InstanceServer.Areas;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public class InstanceTcpClient : TcpClient
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public ClientSession Session { get; private set; }  // semi-persistent data that is the same across connections
        public Area Area { get; set; }

        public InstanceTcpClient()
        {
        }

        public override void OnDataReceived(byte[] buffer, int length)
        {
            //Logger.Debug($"OnDataReceived(): {Convert.ToHexString(buffer.AsSpan(0, length))}");

            List<Packet> packets = new();   // todo: pool this
            Packet.ParseFrom(buffer, length, packets);

            foreach (Packet packet in packets)
            {
                //Logger.Trace($" IN < {packet.Id}");
                ReceivePacket(packet);
            }
        }

        public void OnDisconnected()
        {
            Area?.RemotePlayerManager.RemovePlayer(this);

            if (Session != null && Session.CurrentClient == this)
                InstanceServerApp.Instance.ClientSessionManager.RemoveSession(Session.Id);
        }

        public void Send(Packet packet)
        {
            //Logger.Trace($"OUT > {packet.Id}");
            Connection.Send(packet);
        }

        public void Disconnect()
        {
            Connection.Disconnect();
        }

        #region Handlers

        private void ReceivePacket(Packet packet)
        {
            switch (packet.Id)
            {
                case PacketId.ClientInstanceLoginAttemptPacketId:
                    OnLoginAttempt(packet);
                    break;

                case PacketId.ClientInstanceHeartbeatPacketId:
                    OnHeartbeat();
                    break;

                default:
                    if (Area == null)
                    {
                        Logger.Error("Received non-login packet without an area!");
                        Disconnect();
                        return;
                    }

                    Area.RemotePlayerManager.ReceivePacket(this, packet);
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

            GameConfig config = ConfigManager.Get<GameConfig>();

            ClientSession session = InstanceServerApp.Instance.ClientSessionManager.GetSession(loginAttempt.SessionId);
            if (session.CurrentClient == null)
            {
                session.Id = loginAttempt.SessionId;
                session.CharacterName = loginAttempt.CharacterName;
                session.WorldAreaId = config.WorldAreaId;
                session.StartPosition = new(config.StartPositionX, config.StartPositionY);
            }
            else
            {
                InstanceTcpClient existingClient = session.CurrentClient;
                session.CurrentClient = null;
                existingClient.Disconnect();
            }

            Session = session;
            session.CurrentClient = this;

            InstanceClientLoginAttemptReplyPacket reply = PacketFactory.Get<InstanceClientLoginAttemptReplyPacket>();
            reply.Field0 = 1;
            reply.Field1 = "";
            Send(reply);

            AreaSettings areaSettings = new()
            {
                WorldAreaId = session.WorldAreaId,
                League = "Default",
                Seed = (uint)config.WorldAreaSeed,
            };

            Area area = InstanceServerApp.Instance.AreaManager.GetOrCreateArea(ref areaSettings);
            if (area == null)
            {
                Logger.Error($"Failed to get or create area! worldAreaId={areaSettings.WorldAreaId}, seed={areaSettings.Seed}");
                Disconnect();
                return;
            }

            Area = area;
            Area.RemotePlayerManager.AddPlayer(this);
        }

        private void OnHeartbeat()
        {
            Send(PacketFactory.Get<Packet>(PacketId.InstanceClientHeartbeatReplyPacketId));

#if DEBUG
            if (Area != null)
            {
                InstanceClientServerFrameDurationPacket serverFrameDuration = PacketFactory.Get<InstanceClientServerFrameDurationPacket>();
                serverFrameDuration.ServerFrameTimeMS = (short)Area.LastFrameTime.TotalMilliseconds;
                Send(serverFrameDuration);
            }
#endif
        }

        #endregion
    }
}
