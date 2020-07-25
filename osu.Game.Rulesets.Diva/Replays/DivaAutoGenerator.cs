// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Diva.Objects;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Diva.Replays
{
    public class DivaAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<DivaHitObject> Beatmap => (Beatmap<DivaHitObject>)base.Beatmap;

        public DivaAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            Frames.Add(new DivaReplayFrame());
            foreach (DivaHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new DivaReplayFrame(hitObject.StartTime + hitObject.HitWindows.WindowFor(HitResult.Perfect), hitObject.ValidAction));
                Frames.Add(new DivaReplayFrame(hitObject.StartTime + 1d));
            }

            return Replay;
        }
    }
}
