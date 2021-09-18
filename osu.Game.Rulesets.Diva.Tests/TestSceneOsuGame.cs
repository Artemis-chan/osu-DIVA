// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using osu.Game.Tests.Visual;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Diva.Tests
{
    public class TestSceneOsuGame : OsuTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
			AddGame(new OsuGame());
		}
    }
}
