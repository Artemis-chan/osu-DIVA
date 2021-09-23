// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Skinning;
using osu.Game.Audio;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Diva.Scoring;
using osu.Game.Rulesets.Diva.Objects.Drawables;
using osu.Framework.Input.Events;

namespace osu.Game.Rulesets.Diva.UI
{
    [Cached]
    public class DivaPlayfield : Playfield, IKeyBindingHandler<DivaAction>
    {
        SkinnableSound hitSample;
        private readonly JudgementContainer<DrawableDivaJudgement> judgementLayer;

        private readonly IDictionary<HitResult, DrawablePool<DrawableDivaJudgement>> poolDictionary = new Dictionary<HitResult, DrawablePool<DrawableDivaJudgement>>();

        private readonly Container judgementAboveHitObjectLayer;
        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
                hitSample = new SkinnableSound(new SampleInfo("normal-hitnormal")),
            });
        }

        public DivaPlayfield()
        {
            InternalChildren = new Drawable[]
            {
                judgementLayer = new JudgementContainer<DrawableDivaJudgement> { RelativeSizeAxes = Axes.Both },
                judgementAboveHitObjectLayer = new Container { RelativeSizeAxes = Axes.Both }
            };

            var hitWindows = new DivaHitWindows();
            foreach (var result in Enum.GetValues(typeof(HitResult)).OfType<HitResult>().Where(r => r > HitResult.None && hitWindows.IsHitResultAllowed(r)))
                poolDictionary.Add(result, new DrawableJudgementPool(result, onJudgementLoaded));

            AddRangeInternal(poolDictionary.Values);

            NewResult += onNewResult;
        }

        public bool OnPressed(KeyBindingPressEvent<DivaAction> e)
        {
            this.hitSample.Play();
            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<DivaAction> e)
        {
        }

        private void onJudgementLoaded(DrawableDivaJudgement j)
        {
            judgementAboveHitObjectLayer.Add(j.ProxiedAboveHitObjectsContent);
        }

        private void onNewResult(DrawableHitObject judgedObject, JudgementResult result)
        {
            if (!judgedObject.DisplayResult)
                return;

            DrawableDivaJudgement explosion = poolDictionary[result.Type].Get(doj => doj.Apply(result, judgedObject));
            judgementLayer.Add(explosion);
        }

        private class DrawableJudgementPool : DrawablePool<DrawableDivaJudgement>
        {
            private readonly HitResult result;
            private readonly Action<DrawableDivaJudgement> onLoaded;

            public DrawableJudgementPool(HitResult result, Action<DrawableDivaJudgement> onLoaded)
                : base(10)
            {
                this.result = result;
                this.onLoaded = onLoaded;
            }

            protected override DrawableDivaJudgement CreateNewDrawable()
            {
                var judgement = base.CreateNewDrawable();

                // just a placeholder to initialise the correct drawable hierarchy for this pool.
                judgement.Apply(new JudgementResult(new HitObject(), new Judgement()) { Type = result }, null);

                onLoaded?.Invoke(judgement);

                return judgement;
            }
        }
    }
}
