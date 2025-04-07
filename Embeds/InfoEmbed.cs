using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Utils;

namespace Vaulty.Embeds
{
    public class InfoEmbed : VaultyEmbed
    {
        public InfoEmbed(CommandContext ctx, string name, string description, string version)
        {
            BuildBasicEmbed
            (
                ctx : ctx,
                title: "Bot Information",
                fields: new List<Tuple<string, string, bool>>
                {
                new Tuple<string, string, bool>
                (
                    "Name",
                    name,
                    true
                ),
                new Tuple<string, string, bool>
                (
                    "Version",
                    version,
                    true
                ),
                new Tuple<string, string, bool>
                (
                    "Description",
                    description,
                    false
                )
                },
                color: DiscordColor.Blue
            );

            builder
            .WithThumbnail(App.Vaulty._client.CurrentUser.AvatarUrl) // Bot's profile picture
            .WithFooter($"Requested by {ctx.User.GlobalName}", ctx.User.AvatarUrl)
            .WithTimestamp(DateTime.UtcNow);
        }
    }
}
