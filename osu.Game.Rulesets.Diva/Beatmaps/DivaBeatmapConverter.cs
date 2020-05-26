// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Diva.Objects;
using osuTK;
using System;

namespace osu.Game.Rulesets.Diva.Beatmaps
{
    public class DivaBeatmapConverter : BeatmapConverter<DivaHitObject>
    {
        public DivaBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
            double num = Math.Round((double)beatmap.BeatmapInfo.BaseDifficulty.CircleSize);
            double num2 = Math.Round((double)beatmap.BeatmapInfo.BaseDifficulty.OverallDifficulty);
            float num3 = (float)beatmap.HitObjects.Count((HitObject h) => h is IHasEndTime) / (float)beatmap.HitObjects.Count;
            if ((double)num3 < 0.2)
            {
                this.TargetButtons = 4;
                return;
            }
            if ((double)num3 < 0.3 || num >= 5.0)
            {
                this.TargetButtons = ((num2 > 5.0) ? 4 : 3);
                return;
            }
            if ((double)num3 > 0.6)
            {
                this.TargetButtons = ((num2 > 4.0) ? 2 : 1);
                return;
            }
            this.TargetButtons = 1;
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<DivaHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            yield return new DivaHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = (original as IHasPosition)?.Position ?? Vector2.Zero,
                ValidAction = ValidAction(),
        };
        }

        private DivaAction ValidAction()
        {
            var ac = DivaAction.Circle;

            switch (prevAction)
            {
                case DivaAction.Circle:
                    ac = DivaAction.Cross;
                    break;

                case DivaAction.Cross:
                    ac = DivaAction.Square;
                    break;

                case DivaAction.Square:
                    ac = DivaAction.Triangle;
                    break;
            }

            prevAction = ac;
            return ac;
        }
        

        public int TargetButtons;

        private DivaAction prevAction = DivaAction.Triangle;
    }
}
