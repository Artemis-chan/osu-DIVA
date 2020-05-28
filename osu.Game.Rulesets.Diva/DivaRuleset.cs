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
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Diva.Beatmaps;
using osu.Game.Rulesets.Diva.Mods;
using osu.Game.Rulesets.Diva.UI;
using osu.Game.Rulesets.Diva.Configuration;
using osu.Game.Overlays.Settings;

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

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new DivaModAutoplay() };

                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "diva";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.X, DivaAction.Square),
            new KeyBinding(InputKey.Z, DivaAction.Triangle),
            new KeyBinding(InputKey.A, DivaAction.Cross),
            new KeyBinding(InputKey.S, DivaAction.Circle),
        };

        public override Drawable CreateIcon() => new Sprite
        {
            Texture = new TextureStore(new TextureLoaderStore(CreateResourceStore()), false).Get("Textures/diva"),
        };
    }
}
