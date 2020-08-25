using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Diva.Scoring
{
    public class DivaHitWindows : HitWindows
    {
        private static readonly DifficultyRange[] diva_ranges =
        {
            new DifficultyRange(HitResult.Perfect, 80, 50, -20),
            new DifficultyRange(HitResult.Great, 140, 100, 60),
            new DifficultyRange(HitResult.Meh, 200, 150, 100),
            new DifficultyRange(HitResult.Miss, 300, 350, 400),
        };

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                case HitResult.Meh:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        protected override DifficultyRange[] GetRanges() => diva_ranges;
    }
}