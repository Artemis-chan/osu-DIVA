using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Game.Skinning;
using osuTK;

public class ApproachHand : Container
{
    public override bool RemoveWhenNotAlive
    {
        get
        {
            return false;
        }
    }

    public ApproachHand()
    {
        base.Anchor = Anchor.Centre;
        this.Origin = Anchor.Centre;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore texture)
    {
        base.Child = new SkinnableApproachHand();
    }

    private class SkinnableApproachHand : SkinnableSprite
    {
        public SkinnableApproachHand() : base("Textures/hand", null, ConfineMode.NoScaling)
        {
        }
        protected override Drawable CreateDefault(ISkinComponent component)
        {
            var drawable = base.CreateDefault(component);

            return drawable;
        }
        
    }
}
