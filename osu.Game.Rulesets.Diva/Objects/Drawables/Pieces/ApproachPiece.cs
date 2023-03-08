// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osuTK;
namespace osu.Game.Rulesets.Diva.Objects.Drawables.Pieces
{
	public partial class ApproachPiece : Sprite
    {
        private const float slerp_distance = 150;
        public Vector2 StartPos;
        public void UpdatePos(float blend)
        {
            Position = Extensions.CubicInterpolate(StartPos, Vector2.Zero, blend, slerp_distance);
        }
    }
}
