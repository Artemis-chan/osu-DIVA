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
using osu.Game.Rulesets.Diva.Judgements;
using osu.Game.Rulesets.Judgements;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Diva.Objects.Drawables
{
    public partial class DrawableDivaHitObject : DrawableHitObject<DivaHitObject>, IKeyBindingHandler<DivaAction>
    {
        public const float BASE_SIZE = 56;

        private const double time_preempt = 850;
        private const double time_fadein = 300;
        private const double time_action = 150;

        public override bool HandlePositionalInput => false;

        protected readonly Sprite approachHand;
        protected readonly ApproachPiece approachPiece;

        protected readonly DivaAction validAction;

        protected bool validPress = false;
        protected bool pressed = false;

        protected BindableBool useXB = new BindableBool(false);
        protected BindableBool enableVisualBursts = new BindableBool(true);

        protected override JudgementResult CreateResult(Judgement judgement) => new DivaJudgementResult(HitObject, judgement);

        public DrawableDivaHitObject(DivaHitObject hitObject)
            : base(hitObject)
        {
            Size = new Vector2(BASE_SIZE);

            Origin = Anchor.Centre;
            Position = hitObject.Position;

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
            string textureLocation = GetTextureLocation();

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

        protected virtual string GetTextureLocation() => (useXB.Value) ? "XB/" : "";

        public override IEnumerable<HitSampleInfo> GetSamples() => new[]
        {
            new HitSampleInfo(HitSampleInfo.HIT_NORMAL, SampleControlPoint.DEFAULT_BANK)
        };

        public override void PlaySamples()
        {
        }

        protected static void ApplyMiss(JudgementResult r, DrawableHitObject s) => r.Type = HitResult.Miss;

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (Judged)
            {
                pressed = false;
                return;
            }

            if (!HitObject.HitWindows.CanBeHit(timeOffset) && base.Time.Current > HitObject.StartTime)
            {
                ApplyResult(ApplyMiss);
                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None) return;

            if (pressed && timeOffset > (-time_action))
            {
                if (validPress)
                    ApplyResult((r, s) => r.Type = result);
                else
                    ApplyResult(ApplyMiss);
                pressed = false;
            }
        }

        protected override double InitialLifetimeOffset => time_preempt;

        protected override void UpdateInitialTransforms()
        {
            this.FadeInFromZero(time_fadein);
            this.approachHand.ScaleTo(2, time_fadein, Easing.In);

            this.approachHand.RotateTo(360, time_preempt, Easing.In);
            //this.approachPiece.MoveTo(Vector2.Zero, time_preempt, Easing.None);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:

                    if (enableVisualBursts.Value)
                        this.ScaleTo(2f, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    const double duration = 1000;

                    this.ScaleTo(1.1f, duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, 10), duration, Easing.In);
                    this.FadeColour(Color4.Red.Opacity(0.5f), duration / 2, Easing.OutQuint).Then().FadeOut(duration / 2, Easing.InQuint).Expire();
                    break;
            }
        }

        protected override void Update()
        {
            var b = (float)((Time.Current - LifetimeStart) / time_preempt);
            if (b < 1f)
                this.approachPiece.UpdatePos(b);
        }

        public virtual bool OnPressed(KeyBindingPressEvent<DivaAction> e)
        {
            this.Samples.Play();

            if (Judged)
                return false;

            var action = e.Action switch
            {
                DivaAction.Right => DivaAction.Circle,
                DivaAction.Down => DivaAction.Cross,
                DivaAction.Up => DivaAction.Triangle,
                DivaAction.Left => DivaAction.Square,
                _ => e.Action,
            };
            validPress = action == validAction;
            pressed = true;

            return true;
        }

        public virtual void OnReleased(KeyBindingReleaseEvent<DivaAction> e)
        {
        }
    }
}
