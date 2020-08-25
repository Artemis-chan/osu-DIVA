// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Audio;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Diva.Objects.Drawables.Pieces;
using osuTK;
using osuTK.Graphics;
using osu.Game.Rulesets.Diva.Configuration;
using osu.Framework.Bindables;
using System;

namespace osu.Game.Rulesets.Diva.Objects.Drawables
{
    public class DrawableDivaHitObject : DrawableHitObject<DivaHitObject>, IKeyBindingHandler<DivaAction>
    {
        private const double time_preempt = 850;
        private const double time_fadein = 300;

        public override bool HandlePositionalInput => false;

        private readonly Sprite approachHand;
        private readonly ApproachPiece approachPiece;
        
        private readonly DivaAction validAction;

        private bool validPress = false;
        private bool pressed = false;

        private BindableBool useXB = new BindableBool(false);
        private BindableBool enableVisualBursts = new BindableBool(true);

        public DrawableDivaHitObject(DivaHitObject hitObject)
            : base(hitObject)
        {
            Size = new Vector2(80);

            Origin = Anchor.Centre;
            Position = hitObject.Position;
            Scale = new Vector2(0.7f);

            AddRangeInternal(new Sprite[]
            {
                approachHand = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Rotation = 180f,
                    Depth = 1,
                },
                approachPiece = new ApproachPiece()
                {
                    Depth = 0,
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Position = hitObject.ApproachPieceOriginPosition,
                    StartPos = hitObject.ApproachPieceOriginPosition,
                },
            });

            validAction = hitObject.ValidAction;
        }

        [BackgroundDependencyLoader(true)]
        private void load(TextureStore textures, DivaRulesetConfigManager config)
        {
            config?.BindWith(DivaRulesetSettings.UseXBoxButtons, useXB);
            config?.BindWith(DivaRulesetSettings.EnableVisualBursts, enableVisualBursts);
            string textureLocation = (useXB.Value) ? "XB/" : "";

            AddInternal(new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get($"{textureLocation}{validAction.ToString()}Stat"),
                Depth = 2,
            });

            approachPiece.Texture = textures.Get($"{textureLocation}{validAction.ToString()}Move");
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

        public override void PlaySamples()
        {
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if(Judged || base.Time.Current < HitObject.StartTime) return;

            if (!HitObject.HitWindows.CanBeHit(timeOffset))
            {
                ApplyResult(r => r.Type = HitResult.Miss);
                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            //result = HitResult.Perfect; // for testing cus i'm noob

            if(result == HitResult.None) return;

            if(pressed)
            {
                if(validPress)
                    ApplyResult(r => r.Type = result);
                else if(HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = HitResult.Miss);

                pressed = false;
            }
        }

        protected override double InitialLifetimeOffset => time_preempt;

        protected override void UpdateInitialTransforms()
        {
            this.FadeInFromZero(time_fadein);
            this.approachHand.ScaleTo(1.4f, time_fadein, Easing.In);

            this.approachHand.RotateTo(360, time_preempt, Easing.In);
            //this.approachPiece.MoveTo(Vector2.Zero, time_preempt, Easing.None);
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    

                    if(enableVisualBursts.Value)
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

        protected override void Update()
        {
            var b = (float)((Time.Current - LifetimeStart) / time_preempt);
            if(b < 1f)
                this.approachPiece.UpdatePos(b);
        }

        public bool OnPressed(DivaAction action)
        {
            this.Samples.Play();
            
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
