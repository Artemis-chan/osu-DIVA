// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Diva.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Diva.Objects
{
    public class DivaHitObject : HitObject, IHasPosition
    {
        public override Judgement CreateJudgement() => new Judgement();
        protected override HitWindows CreateHitWindows() => new DivaHitWindows();

        public Vector2 Position { get; set; }

        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Position.Y);
        }

        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(Position.X, value);
        }

        public DivaAction ValidAction;
        public Vector2 ApproachPieceOriginPosition;
    }
}
