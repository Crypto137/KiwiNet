using KiwiNet.Core.Config;
using KiwiNet.Core.Logging;
using KiwiNet.LoginServer.Accounts;
using KiwiNet.LoginServer.Leagues;
using KiwiNet.LoginServer.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Login;
using System.Security.Cryptography;

namespace KiwiNet.LoginServer.Jobs
{
    public sealed class ClientPacketJob : LoginJob
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public LoginClient Client { get; init; }
        public Packet Packet { get; init; }

        public override void Process()
        {
            if (Client.IsConnected)
                ReceivePacket(Client, Packet);
            else
                Logger.Warn($"Received {Packet.Id} from a client that is already disconnected");
        }

        private static void ReceivePacket(LoginClient client, Packet packet)
        {
            switch (packet.Id)
            {
                case PacketId.ClientLoginAuthenticatePacketId:
                    OnAuthenticate(client, packet);
                    break;

                case PacketId.ClientLoginRequestPasswordChangePacketId:
                    OnRequestPasswordChange(client, packet);
                    break;

                case PacketId.ClientLoginRequestDeleteCharacterPacketId:
                    OnRequestDeleteCharacter(client, packet);
                    break;

                case PacketId.ClientLoginChooseCharacterPacketId:
                    OnChooseCharacter(client, packet);
                    break;

                case PacketId.ClientLoginRequestCreateCharacterPacketId:
                    OnRequestCreateCharacter(client, packet);
                    break;

                case PacketId.ClientLoginRequestLeagueListPacketId:
                    OnRequestLeagueList(client);
                    break;

                default:
                    Logger.Warn($"Unhandled packet [{(int)packet.Id}] {packet.Id}");
                    break;
            }
        }

        private static void OnAuthenticate(LoginClient client, Packet packet)
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
                client.Account = account;

            LoginClientAuthenticateReplyPacket reply = new();
            reply.Result = result;

            if (result == BackendError.Success)
            {
                // ProtocolHash appears to change when changes are made to Client.exe.
                // The hash below is for unmodified Client.exe from version 0.8.8 (MD5 9F75A0CCD775ADFC74EBAAA328672CEB)
                reply.ProtocolHash0 = 0xEA48DC36672682AD;
                reply.ProtocolHash1 = 0x80F60792227DA184;
                reply.ProtocolHash2 = 0xEBF9AE66A5A840A1;
                reply.ProtocolHash3 = 0x6A0C854C67F19AEC;
            }

            client.Send(reply);

            if (result == BackendError.Success)
                client.SendCharacterList();
        }

        private static void OnRequestPasswordChange(LoginClient client, Packet packet)
        {
            if (packet is not ClientLoginRequestPasswordChangePacket requestPasswordChange)
            {
                Logger.Warn("OnRequestPasswordChange(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.ChangeAccountPassword(client.Account,
                requestPasswordChange.OldPasswordHash, requestPasswordChange.NewPasswordHash);

            client.SendBackendResult(PacketId.LoginClientRequestPasswordChangeReplyPacketId, result);
        }

        private static void OnRequestDeleteCharacter(LoginClient client, Packet packet)
        {
            if (packet is not StringPacket requestDeleteCharacter)
            {
                Logger.Warn("OnRequestDeleteCharacter(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.DeleteCharacter(client.Account,
                requestDeleteCharacter.Value);

            client.SendBackendResult(PacketId.LoginClientRequestDeleteCharacterReplyPacketId, result);

            if (result == BackendError.Success)
                client.SendCharacterList();
        }

        private static void OnChooseCharacter(LoginClient client, Packet packet)
        {
            if (packet is not StringPacket chooseCharacter)
            {
                Logger.Warn("OnChooseCharacter(): Invalid packet");
                return;
            }

            // TODO: validate

            client.SendBackendResult(PacketId.LoginClientChooseCharacterReplyPacketId, BackendError.Success);

            LoginServerConfig config = ConfigManager.Get<LoginServerConfig>();

            LoginClientInstanceDetailsPacket instanceDetails = PacketFactory.Get<LoginClientInstanceDetailsPacket>();
            instanceDetails.SessionId = 0xDEADBEEF;
            instanceDetails.WorldAreaId = "1_1_1";
            instanceDetails.Entries.Add(new(config.InstanceServer, $"{config.InstanceServerPort}"));
            client.Send(instanceDetails);
        }

        private static void OnRequestCreateCharacter(LoginClient client, Packet packet)
        {
            if (packet is not ClientLoginRequestCreateCharacterPacket requestCreateCharacter)
            {
                Logger.Warn("OnRequestCreateCharacter(): Invalid packet");
                return;
            }

            BackendError result = LoginServerApp.Instance.AccountManager.CreateCharacter(client.Account,
                requestCreateCharacter.Name, requestCreateCharacter.League, requestCreateCharacter.Class);

            client.SendBackendResult(PacketId.LoginClientRequestCreateCharacterReplyPacketId, result);

            if (result == BackendError.Success)
                client.SendCharacterList();
        }

        private static void OnRequestLeagueList(LoginClient client)
        {
            LoginClientLeagueListPacket reply = PacketFactory.Get<LoginClientLeagueListPacket>();

            foreach (League league in LoginServerApp.Instance.LeagueManager)
                reply.Leagues.Add(league.GetLeagueInfo());

            client.Send(reply);
        }
    }
}
