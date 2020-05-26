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
        private const float approach_piece_distance = 1200;

        public DivaBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
            double od = (double)beatmap.BeatmapInfo.BaseDifficulty.OverallDifficulty;
            if (od > 7.0d)
            {
                this.TargetButtons = 4;
                return;
            }
            if (od > 4.5d)
            {
                this.TargetButtons = 3;
                return;
            }
            if (od > 2d)
            {
                this.TargetButtons = 2;
                return;
            }
            this.TargetButtons = 1;

            //Console.WriteLine(this.TargetButtons);
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<DivaHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            var pos = (original as IHasPosition)?.Position ?? Vector2.Zero;
            yield return new DivaHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = pos,
                ValidAction = ValidAction(),
                ApproachPieceOriginPosition = GetApproachPieceOriginPos(pos),
            };
        }

        private DivaAction ValidAction()
        {
            var ac = DivaAction.Circle;

            switch (prevAction)
            {
                case DivaAction.Circle:
                    if(this.TargetButtons < 2) break;
                    ac = DivaAction.Cross;
                    break;

                case DivaAction.Cross:
                    if (this.TargetButtons < 3) break;
                    ac = DivaAction.Square;
                    break;

                case DivaAction.Square:
                    if (this.TargetButtons < 4) break;
                    ac = DivaAction.Triangle;
                    break;
            }

            prevAction = ac;
            return ac;
        }

        private Vector2 GetApproachPieceOriginPos(Vector2 currentObjectPos)
        {
            var dir = (prevObjectPos - currentObjectPos);
            prevObjectPos = currentObjectPos;

            if(dir == Vector2.Zero)
                return new Vector2(approach_piece_distance);

            return dir.Normalized() * approach_piece_distance;            
        }        

        public int TargetButtons;

        private DivaAction prevAction = DivaAction.Triangle;
        private Vector2 prevObjectPos = Vector2.Zero;
    }
}
