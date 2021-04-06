// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Diva.Objects;
using osu.Game.Rulesets.Judgements;

namespace osu.Game.Rulesets.Diva.Judgements {
    public class DivaJudgementResult : JudgementResult {
        /// <summary>
        /// The <see cref="DivaHitObject"/> that was judged.
        /// </summary>
        public new DivaHitObject HitObject => (DivaHitObject)base.HitObject;
        
        
        /// <summary>
        /// The judgement which this <see cref="DivaJudgementResult"/> applies for.
        /// </summary>
        public new Judgement Judgement => base.Judgement;
        
        public DivaJudgementResult(DivaHitObject hitObject, Judgement judgement)
            : base(hitObject, judgement)
        {
        }
    }
}
