using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.Core.Network;
using KiwiNet.LoginServer.Accounts;
using KiwiNet.LoginServer.Leagues;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Login;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace KiwiNet.LoginServer.Network
{
    public sealed class LoginClient : IPacketHandler
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly List<PacketSerializer> _packetSerializers;

        private bool _isConnected;

        public LoginService LoginService { get; }
        public NetworkConnection Connection { get; }
        public Account Account { get; set; }

        public LoginClient(LoginService loginService, NetworkConnection connection)
        {
            _packetSerializers = new() { new ClientPacketSerializer(this) };
            LoginService = loginService;
            Connection = connection;
            _isConnected = true;
        }

        public void Disconnect()
        {
            if (_isConnected == false)
                return;

            Connection.Disconnect();
            LoginService.OnClientDisconnected(Connection);
            _isConnected = false;
        }

        public void Update()
        {
            try
            {
                Connection.Receive();
                PacketSerializer.DeserializeAllPackets(Connection, _packetSerializers);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                Disconnect();
            }
        }

        public void Send(Packet packet)
        {
            if (ConfigManager.Get<LoginServerConfig>().LogPackets)
                Logger.Debug($"OUT > {packet.Id}");

            Connection.Write((byte)packet.Id);
            packet.Serialize(Connection);
            Connection.Flush();
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

        #region IPacketHandler

        public void HandlePacket(NetworkConnection connection, Packet packet)
        {
            if (packet == null)
            {
                Disconnect();
                return;
            }

            switch (packet.Id)
            {
                case PacketId.ClientLoginAuthenticatePacketId:
                    OnAuthenticate(packet);
                    break;

                case PacketId.ClientLoginRequestPasswordChangePacketId:
                    OnRequestPasswordChange(packet);
                    break;

                case PacketId.ClientLoginRequestDeleteCharacterPacketId:
                    OnRequestDeleteCharacter(packet);
                    break;

                case PacketId.ClientLoginChooseCharacterPacketId:
                    OnChooseCharacter(packet);
                    break;

                case PacketId.ClientLoginRequestCreateCharacterPacketId:
                    OnRequestCreateCharacter(packet);
                    break;

                case PacketId.ClientLoginRequestLeagueListPacketId:
                    OnRequestLeagueList();
                    break;

                default:
                    Logger.Warn($"Unhandled packet [{(int)packet.Id}] {packet.Id}");
                    break;
            }
        }

        private void OnAuthenticate(Packet packet)
        {
            if (packet is not ClientLoginAuthenticatePacket authenticate)
            {
                Logger.Warn("OnAuthenticate(): Invalid packet");
                return;
            }

            BackendError result = BackendError.Success;

            AccountManager accountManager = LoginServerApp.Instance.AccountManager;

            Account account = accountManager.GetAccountByName(authenticate.Email);
            if (account != null)
            {
                if (account.PasswordHash != null)
                {
                    // Check password hash for existing accounts
                    if (CryptographicOperations.FixedTimeEquals(account.PasswordHash, authenticate.PasswordHash) == false)
                        result = BackendError.InvalidPassword;
                }
                else
                {
                    // Assign password to password-less accounts
                    account.PasswordHash = authenticate.PasswordHash;
                }
            }
            else
            {
                // Create new accounts for first time logins
                account = accountManager.CreateAccount(authenticate.Email, authenticate.PasswordHash);
            }

            if (result == BackendError.Success)
                Account = account;

            LoginClientAuthenticateReplyPacket reply = new();
            reply.Result = result;

            if (result == BackendError.Success)
            {
                // ProtocolHash appears to change when changes are made to Client.exe.
                // The hash below is for unmodified Client.exe from version 0.8.8 (MD5 9F75A0CCD775ADFC74EBAAA328672CEB)
                Span<ulong> protocolHash = MemoryMarshal.Cast<byte, ulong>(reply.ProtocolHash);
                protocolHash[0] = 0xEA48DC36672682AD;
                protocolHash[1] = 0x80F60792227DA184;
                protocolHash[2] = 0xEBF9AE66A5A840A1;
                protocolHash[3] = 0x6A0C854C67F19AEC;
            }

            Send(reply);

            if (result == BackendError.Success)
                SendCharacterList();
        }

        private void OnRequestPasswordChange(Packet packet)
        {
            if (packet is not ClientLoginRequestPasswordChangePacket requestPasswordChange)
            {
                Logger.Warn("OnRequestPasswordChange(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.ChangeAccountPassword(Account,
                requestPasswordChange.OldPasswordHash, requestPasswordChange.NewPasswordHash);

            SendBackendResult(PacketId.LoginClientRequestPasswordChangeReplyPacketId, result);
        }

        private void OnRequestDeleteCharacter(Packet packet)
        {
            if (packet is not StringPacket requestDeleteCharacter)
            {
                Logger.Warn("OnRequestDeleteCharacter(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.DeleteCharacter(Account,
                requestDeleteCharacter.Value);

            SendBackendResult(PacketId.LoginClientRequestDeleteCharacterReplyPacketId, result);

            if (result == BackendError.Success)
                SendCharacterList();
        }

        private void OnChooseCharacter(Packet packet)
        {
            if (packet is not StringPacket chooseCharacter)
            {
                Logger.Warn("OnChooseCharacter(): Invalid packet");
                return;
            }

            // TODO: validate

            SendBackendResult(PacketId.LoginClientChooseCharacterReplyPacketId, BackendError.Success);

            LoginServerConfig config = ConfigManager.Get<LoginServerConfig>();

            Logger.Info($"Sending instance details for {chooseCharacter.Value}");
            LoginClientInstanceDetailsPacket instanceDetails = PacketFactory.Get<LoginClientInstanceDetailsPacket>();
            instanceDetails.SessionId = 0xDEADBEEF;
            instanceDetails.WorldAreaId = "1_1_1";
            instanceDetails.Entries.Add(new(config.InstanceServer, $"{config.InstanceServerPort}"));
            Send(instanceDetails);
        }

        private void OnRequestCreateCharacter(Packet packet)
        {
            if (packet is not ClientLoginRequestCreateCharacterPacket requestCreateCharacter)
            {
                Logger.Warn("OnRequestCreateCharacter(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.CreateCharacter(Account,
                requestCreateCharacter.Name, requestCreateCharacter.League, requestCreateCharacter.Class);

            SendBackendResult(PacketId.LoginClientRequestCreateCharacterReplyPacketId, result);

            if (result == BackendError.Success)
                SendCharacterList();
        }

        private void OnRequestLeagueList()
        {
            LoginClientLeagueListPacket reply = PacketFactory.Get<LoginClientLeagueListPacket>();

            foreach (League league in LoginServerApp.Instance.LeagueManager)
                reply.Leagues.Add(league.GetLeagueInfo());

            Send(reply);
        }

        #endregion
    }
}
