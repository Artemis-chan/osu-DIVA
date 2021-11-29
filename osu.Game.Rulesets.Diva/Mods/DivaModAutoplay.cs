// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Diva.Objects;
using osu.Game.Rulesets.Diva.Replays;
using osu.Game.Scoring;
using osu.Game.Users;
using osu.Game.Online.API.Requests.Responses;

namespace osu.Game.Rulesets.Diva.Mods
{
    public class DivaModAutoplay : ModAutoplay
    {
        public override Score CreateReplayScore(IBeatmap beatmap, IReadOnlyList<Mod> mods) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new APIUser { Username = "Autoplay" },
            },
            Replay = new DivaAutoGenerator(beatmap).Generate(),
        };
    }
}
