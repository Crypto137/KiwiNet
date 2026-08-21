# KiwiNet

KiwiNet is an experimental server emulator for pre-release versions of Path of Exile (0.x). The only currently supported version is 0.8.8 (beta).

This project is intended for educational and archival purposes only. Support for "modern" versions (3.x+) is not planned.

Feel free to join the [Discord server](https://discord.gg/YNPnrYw2CJ) if you want to discuss this project.

## Features

This is still very far from being "playable".

- Basic login server functionality works: authentication, creating accounts, changing password, creating and deleting characters, sending instance server details.

- Very very early and rudimentary instance server functionality: handling client connections, accepting credentials, sending terrain generation information.

## Notes

- The 0.8.8 client ISO is available on Archive.org as "Path Of Exile PAX 2011 Closed Beta".

- Launch `Client.exe` with the `--nopatch` argument to bypass the patcher.

- The archived client's GGPK file is missing the `Art\Textures\Interface` folder. The missing files can be added to the client directory without modifying the GGPK. `.mat` placeholders can be extracted from the GGPK with [VisualGGPK2](https://github.com/aianlinb/VisualGGPK2) and other tools, `.dds` and `.png` files can be substituted with arbitrary images.

- If the client freezes at the login screen, you need to block access to the `tyypo.com` domain.
  
  - You can do this on Windows by adding `127.0.0.1 tyypo.com` to `C:\Windows\System32\drivers\etc\hosts`.
  
  - This is because the 0.8.8 client is hardcoded to load news from `tyypo.com/production_motd.txt`, and it doesn't handle errors very well.
