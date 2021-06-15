using System;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Diva.Beatmaps;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Diva.Mods
{
	public class DivaModNoDoubles : Mod, IApplicableToBeatmapConverter
	{
		public override string Name => "No Doubles";
		public override string Acronym => "ND";
        public override string Description => @"Only one button at a time.";
		public override ModType Type => ModType.Conversion;
		public override double ScoreMultiplier => 0.667;
		public override bool UserPlayable => true;
		
		public void ApplyToBeatmapConverter(IBeatmapConverter beatmapConverter)
		{
			var bc = (DivaBeatmapConverter)beatmapConverter;

			bc.AllowDoubles = false;
		}
	}
}
