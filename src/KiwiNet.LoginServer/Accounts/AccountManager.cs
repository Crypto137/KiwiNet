using KiwiNet.Core.Logging;
using KiwiNet.LoginServer.Leagues;
using KiwiNet.Protocols.Common;
using System.Security.Cryptography;
using System.Text.Json;

namespace KiwiNet.LoginServer.Accounts
{
    // This is just a placeholder thing to have a little bit of persistence and make life easier for experimenting with the instance server.
    public class AccountManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly Dictionary<ulong, Account> _accounts = new();
        private readonly Dictionary<string, Account> _accountsByEmail = new(StringComparer.OrdinalIgnoreCase);

        private ulong _currentAccountId = 0;
        private ulong _currentCharacterId = 0;

        public bool Initialize()
        {
            try
            {
                LoadAccounts();
                Logger.Info($"Loaded {_accounts.Count} accounts");
            }
            catch (Exception e)
            {
                Logger.Warn($"Failed to load accounts - {e.Message}");

                _accounts.Clear();
                _accountsByEmail.Clear();
                _currentAccountId = 0;
            }

            return true;
        }

        public Account CreateAccount(string email, byte[] password)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);

            ulong id = ++_currentAccountId;
            Account account = new(id, email, $"Exile{id}", password);
            _accounts.Add(account.Id, account);
            _accountsByEmail.Add(account.Email, account);

            SaveAccounts(); // FIXME: use a database

            Logger.Info($"Created account {account}");
            return account;
        }

        public Account GetAccountById(ulong id)
        {
            if (_accounts.TryGetValue(id, out Account account) == false)
                return null;

            return account;
        }

        public Account GetAccountByName(string name)
        {
            if (_accountsByEmail.TryGetValue(name, out Account account) == false)
                return null;

            return account;
        }

        public BackendError ChangeAccountPassword(Account account, byte[] oldPasswordHash, byte[] newPasswordHash)
        {
            if (account == null || _accounts.ContainsKey(account.Id) == false)
                return BackendError.AccountDoesNotExist;

            if (oldPasswordHash == null || oldPasswordHash.Length != 32)
                return BackendError.InvalidPassword;

            if (newPasswordHash == null || newPasswordHash.Length != 32)
                return BackendError.InvalidPassword;

            if (CryptographicOperations.FixedTimeEquals(account.PasswordHash, oldPasswordHash) == false)
                return BackendError.InvalidPassword;

            account.PasswordHash = newPasswordHash;

            SaveAccounts(); // FIXME: use a database

            Logger.Info($"Changed password for account {account}");
            return BackendError.Success;
        }

        public BackendError CreateCharacter(Account account, string characterName, string leagueName, CharacterClass @class)
        {
            if (account == null || _accounts.ContainsKey(account.Id) == false)
                return BackendError.AccountDoesNotExist;

            BackendError characterNameResult = ValidateCharacterName(characterName);
            if (characterNameResult != BackendError.Success)
                return characterNameResult;

            Character existingCharacter = account.GetCharacter(characterName);
            if (existingCharacter != null)
                return BackendError.CharacterNameAlreadyExists;

            BackendError leagueResult = ValidateLeague(ref leagueName);
            if (leagueResult != BackendError.Success)
                return leagueResult;

            switch (@class)
            {
                case CharacterClass.Str:
                case CharacterClass.Dex:
                case CharacterClass.Int:
                case CharacterClass.StrDex:
                case CharacterClass.StrInt:
                    // don't allow test classes (for now)
                    break;

                default:
                    return BackendError.CharacterInvalidClass;
            }

            Character character = new(++_currentCharacterId, characterName, leagueName, @class);
            account.Characters.Add(character);

            SaveAccounts(); // FIXME: use a database

            Logger.Info($"Created character {character}");
            return BackendError.Success;
        }

        public BackendError DeleteCharacter(Account account, string name)
        {
            if (account == null || _accounts.ContainsKey(account.Id) == false)
                return BackendError.AccountDoesNotExist;

            Character character = account.GetCharacter(name);
            if (character == null)
                return BackendError.CharacterDoesNotExist;

            account.Characters.Remove(character);

            SaveAccounts(); // FIXME: use a database

            Logger.Info($"Deleted character {character}");
            return BackendError.Success;
        }

        private void LoadAccounts()
        {
            string accountFilePath = Path.Combine(Environment.CurrentDirectory, "Accounts.json");
            if (File.Exists(accountFilePath) == false)
                return;

            using FileStream fs = File.OpenRead(accountFilePath);

            foreach (Account account in JsonSerializer.Deserialize<Account[]>(fs))
            {
                _accounts.Add(account.Id, account);
                _accountsByEmail.Add(account.Email, account);

                _currentAccountId = Math.Max(account.Id, _currentAccountId);
                foreach (Character character in account.Characters)
                    _currentCharacterId = Math.Max(character.Id, _currentCharacterId);
            }
        }

        private void SaveAccounts()
        {
            string accountFilePath = Path.Combine(Environment.CurrentDirectory, "Accounts.json");

            using FileStream fs = File.Create(accountFilePath);

            JsonSerializer.Serialize(fs, _accounts.Values);
        }

        private static BackendError ValidateCharacterName(string characterName)
        {
            // GGG post from 2016: There is a maximum of number of 23 characters in a character name, also no spaces or numbers can be used in these :)
            // https://www.pathofexile.com/forum/view-thread/1714189
            if (string.IsNullOrWhiteSpace(characterName))
                return BackendError.CharacterNameInvalid;

            if (characterName.Length < 3)    // just a random guess for now
                return BackendError.CharacterNameTooShort;

            if (characterName.Length > 23)
                return BackendError.CharacterNameTooLong;

            if (characterName.Contains(' '))
                return BackendError.CharacterNameInvalid;

            // todo: number check, allow underscore (only one?)

            return BackendError.Success;
        }

        private static BackendError ValidateLeague(ref string leagueName)
        {
            if (string.IsNullOrWhiteSpace(leagueName))
                return BackendError.LeagueNameInvalid;

            League league = LoginServerApp.Instance.LeagueManager.GetLeague(leagueName);
            if (league == null)
                return BackendError.LeagueDoesNotExist;

            // Make sure the league name saved to character uses correct case.
            leagueName = league.Name;
            return BackendError.Success;
        }
    }
}
