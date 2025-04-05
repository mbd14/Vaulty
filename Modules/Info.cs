using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;
using DisCatSharp.Entities;
using DisCatSharp.Enums;

namespace Vaulty.Modules
{
    public class Info : ApplicationCommandsModule
    {
        [SlashCommand("Info", "Get general information about the bot")]
        public async Task Slash_Info(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            {
                Content = "Vaulty (Debug) - 0.0 - Information"
            });
        }

        [SlashCommand("Ufo", "Get general information about the bot")]
        public async Task Slash_ufo(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            {
                Content = "Vaulty (Debug) - 0.0 - Unformation"
            });
        }

        [SlashCommand("Afo", "Get general information about the bot")]
        public async Task Slash_Afo(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            {
                Content = "Vaulty (Debug) - 0.0 - Anformation"
            });
        }
    }
}
