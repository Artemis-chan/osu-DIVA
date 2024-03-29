// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Diva.Mods
{
	public class DivaModKey4 : DivaKeyMod
	{
		public override int KeyCount => 4;
		public override string Name => "Four Buttons";
		public override string Acronym => "4B";
		public override LocalisableString Description => @"Play with four buttons.";
	}
}