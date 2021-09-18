// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
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

namespace osu.Game.Rulesets.Diva.Objects.Drawables
{
    public class DrawableDivaDoubleHitObject : DrawableDivaHitObject
	{
		private const int MAX_COUNT = 2;
		private List<DivaAction> inputs = new List<DivaAction>();
		private readonly DivaAction doubleAction;
		private int inputCount = 0;

		public DrawableDivaDoubleHitObject(DivaHitObject hitObject)
			: base(hitObject)
		{
			doubleAction = (hitObject as DoublePressButton).DoubleAction;
		}

		protected override string GetTextureLocation() => "Doubles/" + base.GetTextureLocation();

		public override bool OnPressed(KeyBindingPressEvent<DivaAction> e)
        {
            this.Samples.Play();

			if (Judged || inputCount > 2)
                return false;


			if(e.Action == validAction || e.Action == doubleAction)
				inputCount++;

			if(inputCount == 2)
			{
				pressed = true;
				validPress = true;
			}

			// inputs.Add(action);
			// inputCount++;

			// if(inputCount == 2)
			// {
			// 	pressed = true;
			// 	validPress = inputs.Contains(validAction) && inputs.Contains(doubleAction);
			// }

			return true;
        }

        // public override void OnReleased(KeyBindingReleaseEvent<DivaAction> e)
        // {
        //     // inputs.Remove(action);
		// 	// inputCount--;
		// }

	}
	
}
