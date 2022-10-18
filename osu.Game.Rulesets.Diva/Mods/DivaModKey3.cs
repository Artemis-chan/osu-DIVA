// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Diva.Mods
{
	public class DivaModKey3 : DivaKeyMod
	{
		public override int KeyCount => 3;
		public override string Name => "Three Buttons";
		public override string Acronym => "3B";
		public override LocalisableString Description => @"Play with three buttons.";
	}
}