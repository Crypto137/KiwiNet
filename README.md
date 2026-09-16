# KiwiNet

KiwiNet is an experimental server emulator for pre-release versions of Path of Exile (0.x). The only currently supported version is 0.8.8 (beta).

This project is intended for educational and archival purposes only. Support for "modern" versions (3.x+) is not planned.

Feel free to join the [Discord server](https://discord.gg/YNPnrYw2CJ) if you want to discuss this project.

## Features

This is currently in very early state with basic sandbox functionality implemented.

- Basic login server functionality: authentication, creating and deleting characters, etc.

- Entering and exploring areas.

- Monster and item spawning.

- Inventory management.

## Setup

These instructions are for Windows.

1. Acquire a copy of the 0.8.8 client. It is available on Archive.org as "Path Of Exile PAX 2011 Closed Beta". 

2. Build the server source code.
   
   1. Install the [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.425-windows-x64-installer).
   
   2. Run the `dotnet build KiwiNet.sln -c Debug` command. `KiwiNet.sln` is located in the `src` folder of this repository.

3. Extract `Data` and `Metadata` folders from the client's GGPK archive with [VisualGGPK2](https://github.com/aianlinb/VisualGGPK2) and place them in the instance server build directory (`KiwiNet/src/KiwiNet.InstanceServer/bin/Debug/net8.0`).

4. Download and extract [interface textures](https://drive.google.com/file/d/13Hu-F3O1sYXO0Ns8ISbAmfFd1ORFotX2/view) to the client directory.

5. Block access to `tyypo.com` by adding `127.0.0.1 tyypo.com` to `C:\Windows\System32\drivers\etc\hosts`.

## Running

1. Run `KiwiNet.LoginServer.exe`.

2. Run `KiwiNet.InstanceServer.exe`.

3. Run `Client.exe` with the `--nopatch` argument.

4. Enter `localhost` in the Realm field on the login screen.

5. Log in with any email and password. An account is automatically created when you log in for the first time with a specific email.
