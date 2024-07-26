// Taken from https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Osu/Objects/Drawables/SkinnableLighting.cs

using osu.Game.Rulesets.Judgements;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Diva.Objects.Drawables
{
    internal partial class SkinnableLighting : SkinnableSprite
    {
        private DrawableDivaJudgement targetObject;
        private JudgementResult targetResult;

        public SkinnableLighting()
            : base("lighting")
        {
        }

        protected override void SkinChanged(ISkinSource skin)
        {
            base.SkinChanged(skin);
            updateColour();
        }

        /// <summary>
        /// Updates the lighting colour from a given hitobject and result.
        /// </summary>
        /// <param name="targetObject">The <see cref="DrawableDivaJudgement"/> that's been judged.</param>
        /// <param name="targetResult">The <see cref="JudgementResult"/> that <paramref name="targetObject"/> was judged with.</param>
        public void SetColourFrom(DrawableDivaJudgement targetObject, JudgementResult targetResult)
        {
            this.targetObject = targetObject;
            this.targetResult = targetResult;

            updateColour();
        }

        private void updateColour()
        {
            if (targetObject == null || targetResult == null)
                Colour = Color4.White;
            else
                Colour = targetResult.IsHit ? targetObject.AccentColour : Color4.Transparent;
        }
    }
}
