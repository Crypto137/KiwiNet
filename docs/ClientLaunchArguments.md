## Client Launch Arguments

## Patcher

From `WinMain()`.

| Argument          | Alias | GGG Description                                                                             | Note |
| ----------------- | ----- | ------------------------------------------------------------------------------------------- | ---- |
| --nopack          | -npk  | Do not load content from a pack file and create any patched content on the real file system |      |
| --nopatch         |       | Do not attempt to patch                                                                     |      |
| --preload         |       | Preload art assets on startup                                                               |      |
| --login-port      | -lp   | The login server port                                                                       |      |
| --patching-server | -p    | The patching server that should be patched from                                             |      |
| --patching-auth   | -pa   | Override basic auth used for for patching content                                           |      |
| --only-generated  | -og   | Only patches generated files from the patching server                                       |      |
| --prev-proc-id    |       | Before running wait for this process id to exit.                                            |      |
| --require-launch  |       | Require that the user press the launch button in the patcher before entering game           |      |
| --use-defaults    | -ud   | Will always use default settings set in the settings.cpp                                    |      |
| --testing-master  | -tm   | The testing client that should be connected to send status messages.                        |      |

## Other

From `sub_5D4A90()`.

| Argument           | Note |
| ------------------ | ---- |
| -adapter           |      |
| -windowed          |      |
| -fullscreen        |      |
| -forcehal          |      |
| -forceref          |      |
| -forcepurehwvp     |      |
| -forcehwvp         |      |
| -forceswvp         |      |
| -forcevsync        |      |
| -width             |      |
| -height            |      |
| -startx            |      |
| -starty            |      |
| -constantframetime |      |
| -quitafterframe    |      |
| -noerrormsgboxes   |      |
| -nostats           |      |
| -relaunchmce       |      |
| -automation        |      |
