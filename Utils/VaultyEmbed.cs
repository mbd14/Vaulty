using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;

namespace Vaulty.Utils
{
    /// <summary>
    /// The VaultyEmbed Class facilitates methods that help building embeds. This class acts as a sort of wrapper to the DiscordEmbedbuilder class
    /// The class facilitiates methods that initialises an embed with basic information such as title, description... and leaves the builder to be modified by the user as he wishes
    /// </summary>
    public class VaultyEmbed
    {
        // Blank builder
        public DiscordEmbedBuilder builder;

        public VaultyEmbed()
        {
            builder = new DiscordEmbedBuilder();
        }

        /// <summary>
        /// Basic embed with only a description and color
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="description"></param>
        /// <param name="color"></param>
        public void BuildBasicEmbed(CommandContext ctx, string description, DiscordColor color)
        {
            builder = new DiscordEmbedBuilder()
                .WithDescription(description)
                .WithColor(color)
                .WithTimestamp(DateTime.UtcNow);
        }


        /// <summary>
        /// Basic embed with title, description, and color. A generic footer is also added
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="color"></param>
        public void BuildBasicEmbed(CommandContext ctx, string title, string description, DiscordColor color)
        {
            builder = new DiscordEmbedBuilder()
                .WithDescription(description)
                .WithColor(color)
                .WithFooter($"Requested by {ctx.User.GlobalName}", ctx.User.AvatarUrl)
                .WithTimestamp(DateTime.UtcNow);
        }

        /// <summary>
        /// Basic embed with title, fields and color.
        /// Typically used to display lists in an embed
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        /// <param name="fields"></param>
        /// <param name="color"></param>
        public void BuildBasicEmbed(CommandContext ctx, string title, List<Tuple<string, string, bool>> fields, DiscordColor color)
        {
            builder = new DiscordEmbedBuilder()
                .WithColor(color)
                .WithTitle(title)
                .WithFooter($"Requested by {ctx.User.GlobalName}", ctx.User.AvatarUrl)
                .WithTimestamp(DateTime.UtcNow);
          

            foreach(var field in fields)
            {
                builder.AddField(field.Item1, field.Item2, field.Item3);
            }
        }

        /// <summary>
        /// Embed with title, description, footer, and color. T
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="footer"></param>
        /// <param name="color"></param>
        public void BuildBasicEmbed(CommandContext ctx, string title, string description, string footer, DiscordColor color)
        {
            builder = new DiscordEmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithFooter(footer)
                .WithColor(color)
                .WithTimestamp(DateTime.UtcNow);
        }

        /// <summary>
        /// Embed with title, description, footer, color, and image
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="footer"></param>
        /// <param name="color"></param>
        /// <param name="imageUrl"></param>
        public void BuildBasicEmbed(CommandContext ctx, string title, string description, string footer, DiscordColor color, string imageUrl)
        {
            builder = new DiscordEmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithFooter(footer)
                .WithColor(color)
                .WithImageUrl(imageUrl)
                .WithTimestamp(DateTime.UtcNow);
        }
    }
}
