# osu!DIVA
A try to recreate Hatsune Miku: Project DIVA as a custom mode for osu!

## Compilation
### Dependencies
* [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core)
* [git](https://git-scm.com/downloads) (optional, you can just download this repo manually)
### Steps
1. Clone the repo
`git clone https://github.com/Artemis-chan/osu-DIVA.git`
2. Get inside **osu-DIVA/osu.Game.Rulesets.Diva** folder
`cd osu-DIVA/osu.Game.Rulesets.Diva`
3. Run `dotnet publish -c Release`
4. The compiled ***osu.Game.Rulesets.Diva.dll*** can be found in **bin/Release/netcoreapp3.1/publish**

## Installation
1. Get the dll from [here](https://github.com/Artemis-chan/osu-DIVA/releases) or compile it yourself
2. Place it in osu! user ruleset folder
    1. Open osu!Lazer
    2. In setting press "Open osu! folder"
    3. Copy the dll into **rulesets** folder
3. Restart osu!
