// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Diva.Beatmaps;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Diva.Mods
{
	public abstract class DivaKeyMod : Mod, IApplicableToBeatmapConverter
	{
		public override string Acronym => Name;
		public abstract int KeyCount { get; }
		public override ModType Type => ModType.Conversion;
		public override double ScoreMultiplier => 1;
		public override bool Ranked => true;

		public void ApplyToBeatmapConverter(IBeatmapConverter beatmapConverter)
		{
			var bc = (DivaBeatmapConverter)beatmapConverter;

			bc.TargetButtons = KeyCount;
		}

		public override Type[] IncompatibleMods => new[]
		{
			typeof(DivaModKey1),
			typeof(DivaModKey2),
			typeof(DivaModKey3),
			typeof(DivaModKey4)
		}.Except(new[] { GetType() }).ToArray();
	}
}