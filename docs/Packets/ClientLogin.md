# Client-Login Packets

Packet ids in the 84-100 range are used for communication between the client and the Login Server.

## ClientLoginAuthenticatePacket

Sent when the client connects.

```c
struct ClientLoginAuthenticatePacket
{
  u8 PacketId; // 84
  wstring Email;
  u8 PasswordHash[32];
};
```

## LoginClientAuthenticateReplyPacket

Server reply for `ClientLoginAuthenticatePacket`.

```c
struct LoginClientAuthenticateReplyPacket
{
  u8 PacketId; // 84
  u8 Result;
  u8 ProtocolHash[32];
};
```

ProtocolHash changes when changes are made to `Client.exe`. Here are known hashes:

- Version 0.8.8 (executable MD5 `9F75A0CCD775ADFC74EBAAA328672CEB`)
  
  - `AD82266736DC48EA84A17D229207F680A140A8A566AEF9EBEC9AF1674C850C6A`

## ClientLoginRequestPasswordChangePacket

Sent by the client when a password change request is made.

```c
struct ClientLoginRequestPasswordChangePacket
{
  u8 PacketId; // 85
  u8 OldPasswordHash[32];
  u8 NewPasswordHash[32]; 
};
```

## LoginClientRequestPasswordChangeReplyPacket

Server reply for `ClientLoginRequestPasswordChangePacket`.

```c
struct LoginClientRequestPasswordChangeReplyPacket
{
  u8 PacketId; // 86
  u8 Result;
};
```

## ClientLoginRequestDeleteCharacterPacket

Sent by the client when a character deletion request is made.

```c
struct ClientLoginRequestDeleteCharacterPacket
{
  u8 PacketId; // 87
  wstring CharacterName;
};
```

## ClientLoginRequestDeleteCharacterPacket

Server reply for `ClientLoginRequestPasswordChangePacket`.

```c
struct ClientLoginRequestDeleteCharacterPacket
{
  u8 PacketId; // 89
  u8 Result;
};
```

## ClientLoginChooseCharacterPacket

Sent by the client when the player successfully creates a new character or picks an existing character from the character list.

```c
struct ClientLoginChooseCharacterPacket
{
  u8 PacketId; // 90
  wstring CharacterName;
};
```

## LoginClientChooseCharacterReplyPacket

Server reply for `ClientLoginChooseCharacterPacket`.

```c
struct LoginClientChooseCharacterReplyPacket
{
  u8 PacketId; // 91
  u8 Result;
};
```

## ClientLoginRequestCreateCharacterPacket

Sent by the client when a character creation request is made.

```c
struct ClientLoginRequestCreateCharacterPacket
{
  u8 PacketId; // 92
  wstring Name;
  wstring League;
  u32 Unknown;
  u32 Unknown;
  u32 Class;
};
```

## LoginClientRequestCreateCharacterReplyPacket

Server reply for `ClientLoginRequestCreateCharacterPacket`.

```c
struct LoginClientRequestCreateCharacterReplyPacket
{
  u8 PacketId; // 93
  u8 Result;
};
```

## LoginClientInstanceDetailsPacket

Sent by the Login Server after a character has been choosen with `ClientLoginChooseCharacterPacket`.

```c
struct InstanceDetailsEntry
{
  string Hostname;
  string Port;   
};

struct LoginClientInstanceDetailsPacket
{
  u8 PacketId; // 94
  u32 SessionId;
  wstring WorldAreaId;
  u8 NumEntries;
  InstanceDetailsEntry Entries[NumEntries];
};
```

`SessionId` is used to authenticate with the Instance Server. `WorldAreaId` is a value from the `id` column in the `WorldAreas` data table. The client proceeds to connect to the specified Instance Server after receiving this message.

## LoginClientCharacterListPacket

Sent by the Login Server when the client successfully authenticates, or when a character is deleted.

```c
struct CharacterInfo
{
  wstring Name;
  wstring League;
  u8 Unknown;
  u32 Level;
  u32 Unknown;
  u8 Class;
};

struct LoginClientCharacterListPacket
{
  u8 PacketId; // 95
  u32 NumCharacters;
  CharacterInfo Characters[NumCharacters];
  u32 Unknown;
};
```

## LoginClientDisconnectPlayerPacket

Sent by the Login Server when a client is disconnected due to a `BackendError`.

```c
struct LoginClientDisconnectPlayerPacket
{
  u8 PacketId; // 96
  u8 Reason;
};
```

## ClientLoginRequestLeagueListPacket

Sent by the client after authentication.

```c
struct ClientLoginRequestLeagueListPacket
{
  u8 PacketId; // 97  
};
```

## LoginClientLeagueListPacketId

Server reply for `ClientLoginRequestLeagueListPacket`.

```c
struct LeagueInfo
{
  wstring Name;
  wstring Description;
  u8 IsHardcore;  
};

struct LoginClientLeagueListPacketId
{
  u8 PacketId; // 98
  u32 NumLeagues;
  LeagueInfo Leagues[NumLeagues];
};
```

## ClientLoginCreateAccountPacket

Sent by the client when an account creation request is made.

```c
struct ClientLoginCreateAccountPacket
{
  u8 PacketId; // 99
  // TODO  
};
```

Note: account creation is disabled in version 0.8.8.

## LoginClientCreateAccountResultPacket

Server reply for `ClientLoginCreateAccountPacket`.

```c
struct LoginClientCreateAccountResultPacket
{
  u8 PacketId; // 100
  u8 Result;
};
```
