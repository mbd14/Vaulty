using System.Threading.Channels;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Vaulty.Embeds;
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

            InfoEmbed embed = new InfoEmbed(ctx, info.name, info.description, info.version);
            
            await ctx.RespondAsync(embed.builder.Build());
        }
    }
}
