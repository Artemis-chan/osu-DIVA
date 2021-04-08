// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Diva.Objects;
using osu.Game.Rulesets.Diva.Objects.Drawables;
using osu.Game.Rulesets.Diva.Replays;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Diva.UI
{
    [Cached]
    public class DrawableDivaRuleset : DrawableRuleset<DivaHitObject>
    {
        public DrawableDivaRuleset(DivaRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new DivaPlayfieldAdjustmentContainer();

        protected override Playfield CreatePlayfield() => new DivaPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new DivaFramedReplayInputHandler(replay);

        public override DrawableHitObject<DivaHitObject> CreateDrawableRepresentation(DivaHitObject h) => new DrawableDivaDoubleHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new DivaInputManager(Ruleset?.RulesetInfo);
    }
}
