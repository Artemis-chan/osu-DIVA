// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Diva
{
    public class DivaDifficultyCalculator : DifficultyCalculator
    {
        public DivaDifficultyCalculator(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate) {
            double od = beatmap.BeatmapInfo.BaseDifficulty.OverallDifficulty;

            double difficulty = 1;
            //TODO: This will need to be rewritten once we start work on #9
            if (od > 6.0d)
            {
                difficulty = 4;
            } else if (od > 4.5d)
            {
                difficulty = 3;
            } else if (od > 2d)
            {
                difficulty = 2;
            }
            
            return new DifficultyAttributes(mods, skills, difficulty);
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate) => Enumerable.Empty<DifficultyHitObject>();

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods) => new Skill[0];
    }
}
