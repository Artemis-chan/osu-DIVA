using System;
using osuTK;

namespace osu.Game.Rulesets.Diva
{
    public static class Extensions
    {
        public static Vector2 Slerp(Vector2 start, Vector2 end, float blend, float offsetDistance)
        {
            if(start == end) return end;

            var pos = Vector2.Lerp(start, end, blend);

            var sdir = (end - start).Normalized().PerpendicularLeft;
            var offsetBlend = 1f - ((0.5f - blend) * MathF.Sign(0.5f - blend)) * 2f;
			pos += sdir * offsetDistance * Math.Clamp(1 + MathF.Log10(offsetBlend), 0, 2);
            //pos += sdir * offsetDistance * offsetBlend;

            return pos;
        }
    }
}