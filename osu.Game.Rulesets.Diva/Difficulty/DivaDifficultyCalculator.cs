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
        public DivaDifficultyCalculator(IRulesetInfo ruleset, IWorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate)
        {
            double od = beatmap.BeatmapInfo.Difficulty.OverallDifficulty;

            //TODO: This will need to be rewritten once we start work on #9
            double difficulty = beatmap.BeatmapInfo.Difficulty.OverallDifficulty switch
            {
                >= 6.0f => 4,
                >= 4.5f => 3,
                >= 2f => 2,
                _ => 1,
            };

            return new DifficultyAttributes(mods, difficulty);
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate) => Enumerable.Empty<DifficultyHitObject>();

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods, double clockRate) => new Skill[0];
    }
}
