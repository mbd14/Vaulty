using DSharpPlus.Commands;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.SlashCommands;
using Vaulty.Database.Models;
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

        [Command("cooldown")]
        [TextAlias("cd")]
        public async Task CooldownCommand(CommandContext ctx)
        {
            
            User u = new User() { Id = ctx.User.Id.ToString()};
            CommandExecutions commandExecutions = new CommandExecutions() { Id = ctx.User.Id.ToString()};

            u.ReadUser();
            commandExecutions.GetExecution();


            CooldownEmbed response = new CooldownEmbed(ctx, u, commandExecutions);

            await ctx.RespondAsync(response.builder.Build());

        }
    }
}
