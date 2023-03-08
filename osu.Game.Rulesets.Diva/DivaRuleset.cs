// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Osu.Mods;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Diva.Beatmaps;
using osu.Game.Rulesets.Diva.Mods;
using osu.Game.Rulesets.Diva.UI;
using osu.Game.Rulesets.Diva.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Beatmaps.Legacy;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Rendering;

namespace osu.Game.Rulesets.Diva
{
    public class DivaRuleset : Ruleset
    {
        public override string Description => "osu!DIVA";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableDivaRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new DivaBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
            new DivaDifficultyCalculator(RulesetInfo, beatmap);

        public override RulesetSettingsSubsection CreateSettings() =>
            new DivaSettingsSubsection(this);

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) =>
            new DivaRulesetConfigManager(settings, RulesetInfo);

        public override IEnumerable<Mod> ConvertFromLegacyMods(LegacyMods mods)
        {
            if (mods.HasFlag(LegacyMods.Nightcore))
                yield return new OsuModNightcore();
            else if (mods.HasFlag(LegacyMods.DoubleTime))
                yield return new OsuModDoubleTime();

            if (mods.HasFlag(LegacyMods.Perfect))
                yield return new OsuModPerfect();
            else if (mods.HasFlag(LegacyMods.SuddenDeath))
                yield return new OsuModSuddenDeath();

            if (mods.HasFlag(LegacyMods.Autopilot))
                yield return new OsuModAutopilot();

            if (mods.HasFlag(LegacyMods.Cinema))
                yield return new OsuModCinema();
            else if (mods.HasFlag(LegacyMods.Autoplay))
                yield return new OsuModAutoplay();

            if (mods.HasFlag(LegacyMods.Easy))
                yield return new OsuModEasy();

            if (mods.HasFlag(LegacyMods.Flashlight))
                yield return new OsuModFlashlight();

            if (mods.HasFlag(LegacyMods.HalfTime))
                yield return new OsuModHalfTime();

            if (mods.HasFlag(LegacyMods.HardRock))
                yield return new OsuModHardRock();

            if (mods.HasFlag(LegacyMods.Hidden))
                yield return new OsuModHidden();

            if (mods.HasFlag(LegacyMods.NoFail))
                yield return new OsuModNoFail();

            if (mods.HasFlag(LegacyMods.Relax))
                yield return new OsuModRelax();

            if (mods.HasFlag(LegacyMods.SpunOut))
                yield return new OsuModSpunOut();

            if (mods.HasFlag(LegacyMods.Target))
                yield return new OsuModTargetPractice();

            if (mods.HasFlag(LegacyMods.TouchDevice))
                yield return new OsuModTouchDevice();
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new Mod[] { new DivaModAutoplay() };

                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new OsuModNoFail(),
                        new MultiMod(new OsuModHalfTime(), new OsuModDaycore()),
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new OsuModSuddenDeath(),
                        new MultiMod(new OsuModDoubleTime(), new OsuModNightcore()),
                    };

                case ModType.Conversion:
                    return new Mod[]
                    {
                        new OsuModDifficultyAdjust(),
                        new MultiMod(
                            new DivaModKey1(),
                            new DivaModKey2(),
                            new DivaModKey3(),
                            new DivaModKey4()
                        ),
                        new DivaModNoDoubles(),
                    };

                default:
                    return Array.Empty<Mod>();
            }
        }

        public override string ShortName => "diva";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.A, DivaAction.Square),
            new KeyBinding(InputKey.W, DivaAction.Triangle),
            new KeyBinding(InputKey.S, DivaAction.Cross),
            new KeyBinding(InputKey.D, DivaAction.Circle),

            new KeyBinding(InputKey.Left, DivaAction.Left),
            new KeyBinding(InputKey.Up, DivaAction.Up),
            new KeyBinding(InputKey.Right, DivaAction.Right),
            new KeyBinding(InputKey.Down, DivaAction.Down)
        };

        public override Drawable CreateIcon() => new DivaRulesetIcon(this);

		public partial class DivaRulesetIcon : Sprite
		{
			private readonly Ruleset ruleset;

			public DivaRulesetIcon(Ruleset ruleset)
			{
				this.ruleset = ruleset;
			}

			[BackgroundDependencyLoader]
			private void load(IRenderer renderer)
			{
				Texture = new TextureStore(renderer, new TextureLoaderStore(ruleset.CreateResourceStore()), false).Get("Textures/diva");
			}
		}
    }
}
