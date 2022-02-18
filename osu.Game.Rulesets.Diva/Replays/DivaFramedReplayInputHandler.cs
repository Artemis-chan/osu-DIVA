// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Diagnostics;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Diva.Replays
{
    public class DivaFramedReplayInputHandler : FramedReplayInputHandler<DivaReplayFrame>
    {
        public DivaFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(DivaReplayFrame frame) => frame.Actions.Count > 0;

        protected override void CollectReplayInputs(List<IInput> inputs)
        {
            inputs.Add(new ReplayState<DivaAction>()
            {
                PressedActions = CurrentFrame?.Actions ?? new List<DivaAction>(),
            });
        }
    }
}
