// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Tournament.Models;
using osuTK;

namespace osu.Game.Tournament.Components
{
    public class DrawableTeamFlag : Container
    {
        private readonly TournamentTeam team;

        [UsedImplicitly]
        private Bindable<string> flag;

        private Sprite flagSprite;

        private int[] DEFAULT_SIZES = {75, 75};

        private int[] sizes;

        public DrawableTeamFlag(TournamentTeam team)
        {
            this.team = team;
            this.sizes = DEFAULT_SIZES;
        }

        public DrawableTeamFlag(TournamentTeam team, int[] sizes)
        {
            this.team = team;
            this.sizes = sizes.Length < 2 ? DEFAULT_SIZES : sizes;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            if (team == null) return;

            Size = new Vector2(this.sizes[0], this.sizes[1]);
            Masking = true;
            CornerRadius = 5;
            Child = flagSprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                FillMode = FillMode.Fill
            };

            (flag = team.FlagName.GetBoundCopy()).BindValueChanged(acronym => flagSprite.Texture = textures.Get($@"Flags/{team.FlagName}"), true);
        }
    }
}
