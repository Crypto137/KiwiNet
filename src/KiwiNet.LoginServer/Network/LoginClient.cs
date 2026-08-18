using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network.Tcp;
using KiwiNet.LoginServer.Accounts;
using KiwiNet.LoginServer.Jobs;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Login;

namespace KiwiNet.LoginServer.Network
{
    public class LoginClient : TcpClient
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public bool IsConnected { get => Connection.Connected; }

        public Account Account { get; set; }

        public LoginClient()
        {
        }

        public override void OnDataReceived(byte[] buffer, int length)
        {
            // We can parse async without issues, but do the actual handling
            // on a dedicated worker thread to avoid multithreading weirdness.
            List<Packet> packets = new();   // todo: pool this
            Packet.ParseFrom(buffer, length, packets);

            foreach (Packet packet in packets)
            {
                ClientPacketJob job = new() { Client = this, Packet = packet };
                LoginServerApp.Instance.JobQueue.Enqueue(job);

                if (ConfigManager.Get<LoginServerConfig>().LogPackets)
                    Logger.Debug($" IN < {packet.Id}");
            }
        }

        public void Send(Packet packet)
        {
            if (ConfigManager.Get<LoginServerConfig>().LogPackets)
                Logger.Debug($"OUT > {packet.Id}");

            Connection.Send(packet);
        }

        public void SendBackendResult(PacketId packetId, BackendError result)
        {
            BackendErrorPacket reply = PacketFactory.Get<BackendErrorPacket>(packetId);
            reply.Value = result;
            Send(reply);
        }

        public void SendCharacterList()
        {
            LoginClientCharacterListPacket characterList = PacketFactory.Get<LoginClientCharacterListPacket>();

            if (Account != null)
            {
                foreach (Character character in Account.Characters)
                    characterList.Characters.Add(character.GetCharacterInfo());
            }

            characterList.Field1 = 0;
            Send(characterList);
        }
    }
}
