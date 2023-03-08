using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Diva.Configuration
{
    public class DivaRulesetConfigManager : RulesetConfigManager<DivaRulesetSettings>
    {
        public DivaRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            SetDefault(DivaRulesetSettings.UseXBoxButtons, false);
            SetDefault(DivaRulesetSettings.EnableVisualBursts, true);
        }
    }

    public enum DivaRulesetSettings
    {
        UseXBoxButtons,
        EnableVisualBursts
    }
}