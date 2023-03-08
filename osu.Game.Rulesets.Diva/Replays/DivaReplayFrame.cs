// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Diva.Replays
{
    public class DivaReplayFrame : ReplayFrame
    {
        public List<DivaAction> Actions = new List<DivaAction>();

        public DivaReplayFrame()
        {
        }

        public DivaReplayFrame(double time, params DivaAction[] actions) : base(time)
        {
            Actions.AddRange(actions);
        }
    }
}
