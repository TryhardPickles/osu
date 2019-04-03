// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Online.Chat;
using osu.Game.Online.Leaderboards;
using osu.Game.Rulesets.UI;
using osu.Game.Scoring;
using osu.Game.Users;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Overlays.BeatmapSet.Scores
{
    public class ScoreTableScoreRow : ScoreTableRow
    {
        private readonly int index;
        private readonly ScoreInfo score;

        public ScoreTableScoreRow(int index, ScoreInfo score)
        {
            this.index = index;
            this.score = score;
        }

        protected override Drawable CreateIndexCell() => new OsuSpriteText
        {
            Text = $"#{index + 1}",
            Font = OsuFont.GetFont(size: TEXT_SIZE, weight: FontWeight.Bold)
        };

        protected override Drawable CreateRankCell() => new DrawableRank(score.Rank)
        {
            Size = new Vector2(30, 20),
        };

        protected override Drawable CreateScoreCell() => new OsuSpriteText
        {
            Text = $@"{score.TotalScore:N0}",
            Font = OsuFont.GetFont(size: TEXT_SIZE, weight: index == 0 ? FontWeight.Bold : FontWeight.Medium)
        };

        protected override Drawable CreateAccuracyCell() => new OsuSpriteText
        {
            Text = $@"{score.Accuracy:P2}",
            Font = OsuFont.GetFont(size: TEXT_SIZE),
            Colour = score.Accuracy == 1 ? Color4.GreenYellow : Color4.White
        };

        protected override Drawable CreatePlayerCell()
        {
            var username = new LinkFlowContainer(t => t.Font = OsuFont.GetFont(size: TEXT_SIZE))
            {
                AutoSizeAxes = Axes.Both,
            };

            username.AddLink(score.User.Username, null, LinkAction.OpenUserProfile, score.User.Id.ToString(), "Open profile");

            return new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(5, 0),
                Children = new Drawable[]
                {
                    new DrawableFlag(score.User.Country) { Size = new Vector2(20, 13) },
                    username
                }
            };
        }

        protected override IEnumerable<Drawable> CreateStatisticsCells()
        {
            yield return new OsuSpriteText
            {
                Text = $@"{score.MaxCombo:N0}x",
                Font = OsuFont.GetFont(size: TEXT_SIZE)
            };

            foreach (var kvp in score.Statistics)
            {
                yield return new OsuSpriteText
                {
                    Text = $"{kvp.Value}",
                    Font = OsuFont.GetFont(size: TEXT_SIZE),
                    Colour = kvp.Value == 0 ? Color4.Gray : Color4.White
                };
            }
        }

        protected override Drawable CreatePpCell() => new OsuSpriteText
        {
            Text = $@"{score.PP:N0}",
            Font = OsuFont.GetFont(size: TEXT_SIZE)
        };

        protected override Drawable CreateModsCell() => new FillFlowContainer
        {
            Direction = FillDirection.Horizontal,
            AutoSizeAxes = Axes.Both,
            ChildrenEnumerable = score.Mods.Select(m => new ModIcon(m)
            {
                AutoSizeAxes = Axes.Both,
                Scale = new Vector2(0.3f)
            })
        };
    }
}
