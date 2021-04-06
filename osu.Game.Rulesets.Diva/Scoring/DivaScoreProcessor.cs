// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Diva.Judgements;
using osu.Game.Rulesets.Diva.Objects;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Diva.Scoring
{
    public class DivaScoreProcessor : ScoreProcessor
    {
        protected override JudgementResult CreateResult(HitObject hitObject, Judgement judgement) =>
            new DivaJudgementResult((DivaHitObject)hitObject, judgement);
    }
}
