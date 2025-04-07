using System.Threading.Channels;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    public class InfoModule : ApplicationCommandModule
    {
        /// <summary>
        /// Get misc. information on the bot.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("info")]
        public async Task InfoCommand(CommandContext ctx)
        {

            var info = JsonSensitiveLoader.BotInfoLoad();

            var embed = new DiscordEmbedBuilder()
            .WithTitle("Bot Information")
            .WithColor(DiscordColor.Cyan)
            .WithThumbnail(App.Vaulty._client.CurrentUser.AvatarUrl) // Bot's profile picture
            .AddField("Name", info.name, true)
            .AddField("Version", info.version, true)
            .AddField("Description", info.description)
            .WithFooter($"Requested by {ctx.User.GlobalName}", ctx.User.AvatarUrl)
            .WithTimestamp(DateTime.UtcNow);

            // Send the embed
            await ctx.RespondAsync(embed);
        }
    }
}
