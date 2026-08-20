# Common Structures and Enums

This document describes common data structures and enums used in various protocols.

## General Notes

- All integers are sent in the big-endian order.

- There are no packet size fields or delimiters.

## Structures

### wstring

A UTF-16 encoded string. Used for the vast majority of strings.

```c
struct wstring
{
  u16 Length;
  u16 Chars;
};
```

## string

An ASCII encoded string. Used for strings that are not visible in the user interface (e.g. Instance Server connection information).

```c
struct string
{
  u16 Length;
  u8 Chars;  
};
```

## Enums

### BackendError

Used as a result code for various operations (authentication, character management, etc.).

```c
enum BackendError
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
};
```

### CharacterClass

```c
enum CharacterClass
{
  StrDexInt,
  Str,
  Dex,
  Int,
  StrDex,
  StrInt,
  DexInt,
  // TODO: test classes after this
};
```
