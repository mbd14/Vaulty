using System.Threading.Channels;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

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
            var embed = new DiscordEmbedBuilder()
            .WithTitle("Bot Information")
            .WithColor(DiscordColor.Cyan)
            .WithThumbnail(App.Vaulty._client.CurrentUser.AvatarUrl) // Bot's profile picture
            .AddField("Name", App.Vaulty._client.CurrentUser.Username, true)
            .AddField("Version", "Alpha 0.1", true)
            .AddField("Description", "A feature-rich economy discord bot")
            .WithFooter($"Requested by {ctx.User.GlobalName}", ctx.User.AvatarUrl)
            .WithTimestamp(DateTime.UtcNow);

            // Send the embed
            await ctx.RespondAsync(embed);
        }
    }
}
