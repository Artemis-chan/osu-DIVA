// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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

namespace osu.Game.Rulesets.Diva
{
    public class DivaRuleset : Ruleset
    {
        public override string Description => "osu!DIVA";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
            new DrawableDivaRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new DivaBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) =>
            new DivaDifficultyCalculator(this, beatmap);

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
                yield return new OsuModTarget();

            if (mods.HasFlag(LegacyMods.TouchDevice))
                yield return new OsuModTouchDevice();
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new DivaModAutoplay() };

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
                    };

                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "diva";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.A, DivaAction.Square),
            new KeyBinding(InputKey.W, DivaAction.Triangle),
            new KeyBinding(InputKey.S, DivaAction.Cross),
            new KeyBinding(InputKey.D, DivaAction.Circle),
            
            new KeyBinding(InputKey.J, DivaAction.Square),
            new KeyBinding(InputKey.I, DivaAction.Triangle),
            new KeyBinding(InputKey.K, DivaAction.Cross),
            new KeyBinding(InputKey.L, DivaAction.Circle),
        };

        public override Drawable CreateIcon() => new Sprite
        {
            Texture = new TextureStore(new TextureLoaderStore(CreateResourceStore()), false).Get("Textures/diva"),
        };
    }
}
