# osu!DIVA

A try to recreate Hatsune Miku: Project DIVA as a custom mode for osu!

## Installation

1. Get the dll from:
    - [Releases](https://github.com/Artemis-chan/osu-DIVA/releases)
    - If that does not work, [GitHub Actions](https://nightly.link/Artemis-chan/osu-DIVA/workflows/debug/master)
    - or [compile it yourself](#build)
2. Place it in osu! user ruleset folder
    1. Open osu!Lazer
    2. In setting press "Open osu! folder"
    3. Copy the dll into **rulesets** folder
3. Restart osu!

## Build

### Dependencies

- [.NET 8.0(min) SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
- [git](https://git-scm.com/downloads) (optional, you can just download this repo manually)

### Steps

1. Clone the repo
`git clone https://github.com/Artemis-chan/osu-DIVA.git`
2. Get inside **osu-DIVA/osu.Game.Rulesets.Diva** folder
`cd osu-DIVA/osu.Game.Rulesets.Diva`
3. Run `dotnet publish -c Release`
4. The compiled ***osu.Game.Rulesets.Diva.dll*** can be found in **bin/Release/net6.0/publish**
