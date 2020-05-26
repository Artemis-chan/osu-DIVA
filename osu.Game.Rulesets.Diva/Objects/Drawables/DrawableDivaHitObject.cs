// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Audio;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;
using osu.Game.Rulesets.Diva.Configuration;
using osu.Framework.Bindables;

namespace osu.Game.Rulesets.Diva.Objects.Drawables
{
    public class DrawableDivaHitObject : DrawableHitObject<DivaHitObject>, IKeyBindingHandler<DivaAction>
    {
        private const double time_preempt = 600;
        private const double time_fadein = 400;

        public override bool HandlePositionalInput => true;

        private readonly Sprite approachHand;

        private DivaAction validAction;
        private bool validPress = false;
        private bool pressed = false;

        private BindableBool useXB = new BindableBool(false);

        public DrawableDivaHitObject(DivaHitObject hitObject)
            : base(hitObject)
        {
            Size = new Vector2(80);

            Origin = Anchor.Centre;
            Position = hitObject.Position;

            AddRangeInternal(new Sprite[]
            {
                approachHand = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Depth = 0,
                },
            });

            validAction = hitObject.ValidAction;
        }

        [BackgroundDependencyLoader(true)]
        private void load(TextureStore textures, DivaRulesetConfigManager config)
        {
            config?.BindWith(DivaRulesetSettings.UseXBoxButtons, useXB);
            string textureLocation = (useXB.Value) ? "XB/" : "";

            AddInternal(new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get($"{textureLocation}{validAction.ToString()}Stat"),
                Depth = 1,
                Scale = new Vector2(0.7f),
            });
            approachHand.Texture = textures.Get("hand");

        }

        public override double LifetimeStart
        {
            get => base.LifetimeStart;
            set
            {
                base.LifetimeStart = value;
            }
        }
        public override double LifetimeEnd
        {
            get
            {
                return base.LifetimeEnd;
            }
            set
            {
                base.LifetimeEnd = value;
            }
        }

        public override IEnumerable<HitSampleInfo> GetSamples() => new[]
        {
            new HitSampleInfo
            {
                Bank = SampleControlPoint.DEFAULT_BANK,
                Name = HitSampleInfo.HIT_NORMAL,
            }
        };

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if(Judged) return;

            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                {
                    ApplyResult(r => r.Type = HitResult.Miss);
                    return;
                }
                
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            //result = HitResult.Perfect; // for testing cus i'm noob

            if(result == HitResult.None) return;

            if(pressed)
            {
                if(validPress)
                    ApplyResult(r => r.Type = result);
                else
                    ApplyResult(r => r.Type = HitResult.Miss);

                pressed = false;
            }
        }

        protected override double InitialLifetimeOffset => time_preempt;

        protected override void UpdateInitialTransforms()
        {
            this.FadeInFromZero(time_fadein);
            this.approachHand.ScaleTo(1f, time_fadein, Easing.In);
            this.approachHand.Rotation = 180f;
            this.approachHand.RotateTo(360, time_preempt, Easing.In);
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            switch (state)
            {                    
                case ArmedState.Hit:
                    this.ScaleTo(5, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    const double duration = 1000;

                    this.ScaleTo(0.8f, duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, 10), duration, Easing.In);
                    this.FadeColour(Color4.Red.Opacity(0.5f), duration / 2, Easing.OutQuint).Then().FadeOut(duration / 2, Easing.InQuint).Expire();
                    break;
            }
        }

        public bool OnPressed(DivaAction action)
        {
            if (Judged)
                return false;

            validPress = action == validAction;
            pressed = true;

            return true;
        }

        public void OnReleased(DivaAction action)
        {
        }
    }
}
