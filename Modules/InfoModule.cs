using DSharpPlus.Commands;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.SlashCommands;
using Vaulty.Embeds;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    /// <summary>
    /// Represents an embed that displays the shop. Inherits from the generic VaultyEmbed
    /// </summary>
    public class InfoModule
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
