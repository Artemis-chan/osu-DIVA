// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Skinning;
using osu.Game.Audio;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Diva.UI
{
    [Cached]
    public class DivaPlayfield : Playfield, IKeyBindingHandler<DivaAction>
    {
        SkinnableSound hitSample;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
                hitSample = new SkinnableSound(new SampleInfo("normal-hitnormal")),
            });
        }
        
        public bool OnPressed(DivaAction action)
        {
            this.hitSample.Play();
            return true;
        }

        public void OnReleased(DivaAction action)
        {
        }
    }
}
