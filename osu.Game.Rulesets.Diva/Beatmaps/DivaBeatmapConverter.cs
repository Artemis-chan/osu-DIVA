// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Diva.Objects;
using osuTK;
using System.Threading;

namespace osu.Game.Rulesets.Diva.Beatmaps
{
    public class DivaBeatmapConverter : BeatmapConverter<DivaHitObject>
    {
        //todo:
        //make single position bursts to a line pattern
        //every approach piece of a combo will come from one direction
        //create patterns of same button

        public int TargetButtons;
        public bool AllowDoubles = true;

        private DivaAction prevAction = DivaAction.Triangle;

        private Vector2 prevObjectPos = Vector2.Zero;

        private float osuObjectSize = 0;
        private int streamLength = 0;
        //these variables were at the end of the class, such heresy had i done

        private const float approach_piece_distance = 1200;

        public DivaBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
            this.TargetButtons = beatmap.BeatmapInfo.Difficulty.OverallDifficulty switch
            {
                >= 6.0f => 4,
                >= 4.5f => 3,
                >= 2f => 2,
                _ => 1,
            };

            osuObjectSize = (54.4f - 4.48f *  beatmap.Difficulty.CircleSize) * 2;

            //Console.WriteLine(this.TargetButtons);
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        protected override IEnumerable<DivaHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            //not sure if handling the cancellation is needed, as offical modes doesnt handle *scratches my head* or even its possible
            var pos = (original as IHasPosition)?.Position ?? Vector2.Zero;
            var newCombo = (original as IHasCombo)?.NewCombo ?? true;

            //currently press presses are placed in place of sliders as placeholder, but arcade slider are better suited for these
            //another option would be long sliders: arcade sliders, short sliders: doubles
            if (AllowDoubles && original is IHasPathWithRepeats)
            {
                yield return new DoublePressButton
                {
                    Samples = original.Samples,
                    StartTime = original.StartTime,
                    Position = pos,
                    ValidAction = ValidAction(pos, newCombo),
                    DoubleAction = DoubleAction(prevAction),
                    ApproachPieceOriginPosition = GetApproachPieceOriginPos(pos),
                };
            }
            else
            {
                yield return new DivaHitObject
                {
                    Samples = original.Samples,
                    StartTime = original.StartTime,
                    Position = pos,
                    ValidAction = ValidAction(pos, newCombo),
                    ApproachPieceOriginPosition = GetApproachPieceOriginPos(pos),
                };
            }

        }

        private static DivaAction DoubleAction(DivaAction ac) => ac switch
        {
            DivaAction.Circle => DivaAction.Right,
            DivaAction.Cross => DivaAction.Down,
            DivaAction.Square => DivaAction.Left,
            _ => DivaAction.Up
        };

        //placeholder
        private DivaAction ValidAction(Vector2 currentObjectPos, bool newCombo)
        {
            var distance = (prevObjectPos - currentObjectPos).Length;
            if (distance < osuObjectSize * 1.2 && (streamLength < 20 || !newCombo))
            {
                streamLength++;
                return prevAction;
            }
            else
            {
                streamLength = 0;
            }

            var ac = DivaAction.Circle;

            switch (prevAction)
            {
                case DivaAction.Circle:
                    if (this.TargetButtons < 2) break;
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

            if (dir == Vector2.Zero)
                return new Vector2(approach_piece_distance);

            return dir.Normalized() * approach_piece_distance;
        }
    }
}
