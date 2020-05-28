// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Diva.Configuration;

namespace osu.Game.Rulesets.Diva.UI
{
    public class DivaSettingsSubsection : RulesetSettingsSubsection
    {
        protected override string Header => "osu!DIVA";

        public DivaSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (DivaRulesetConfigManager)Config;

            if (config == null)
                return;

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Use XBox Button Icons",
                    Bindable = config.GetBindable<bool>(DivaRulesetSettings.UseXBoxButtons)
                }
            };
        }
    }
}
