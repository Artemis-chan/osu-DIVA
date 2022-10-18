// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Rulesets.Diva.Mods
{
	public class DivaModKey2 : DivaKeyMod
	{
		public override int KeyCount => 2;
		public override string Name => "Two Buttons";
		public override string Acronym => "2B";
		public override LocalisableString Description => @"Play with two buttons.";
	}
}