using KiwiNet.Core.Extensions;

namespace KiwiNet.Protocols.Packets.Common
{
    public enum BackendError : byte
    {
        Success,
        AccountNameAlreadyExists,
        AccountNameInvalid,
        EmailInvalid,
        EmailAlreadyExists,
        AccountDoesNotExist,
        InvalidPassword,
        AlreadyLoggedOn,
        InvalidProtocolVersion,
        NotAllowedGameLogin,
        OtherAccountLoggedOn,
        AccountNotLoggedIn,
        Timeout,
        Disconnected,
        TerrainGenerationOutOfSync,
        UnexpectedDisconnect,
        CharacterDoesNotExist,
        NotCharacterOwner,
        LinkedItemDoesNotExist,
        DatabaseError,
        TransferedToInstance,
        CharacterNameAlreadyExists,
        CharacterNameInvalid,
        CharacterNameTooShort,
        CharacterNameTooLong,
        CharacterInvalidClass,
        LeagueDoesNotExist,
        InvalidCharacterFlags,
        LeagueNameAlreadyExists,
        LeagueNameInvalid,
        LeagueDescriptionInvalid,
        AlreadyInParty,
        NotItemOwner,
        NotAPartyMember,
        PartyPromotionRequired,
        PartyDoesNotExist,
        ServiceDoesNotExist,
        CommandInvalid,
        DestinationUnreachable,
        NotInPartyWith,
        NotInParty,
        InstanceFull,
        InvalidInstanceTransfer,
        PortalDoesNotExist,
        OutOfYourLeague,
        DownForMaintenance,
        InstanceAuthenticationInvalid,
        PartyFull,
        TooManyCharacters,
        InvalidActivationKey,
        EmailValidationRequired,
        MissingRequiredField,
        InvalidArgument,
    }

    public sealed class BackendErrorPacket : Packet
    {
        public BackendError Value { get; set; }

        public BackendErrorPacket(PacketId id) : base(id)
        {
        }

        protected override void DeserializeData(Stream stream)
        {
            Value = stream.Read<BackendError>();
        }

        protected override void SerializeData(Stream stream)
        {
            stream.Write(Value);
        }
    }
}
