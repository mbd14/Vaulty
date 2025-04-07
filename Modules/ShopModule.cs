using DSharpPlus.Commands;
using Vaulty.Database.Models;
using Vaulty.Embeds;


namespace Vaulty.Modules
{
    public class ShopModule
    {
        /// <summary>
        /// Displays the shop in the chat
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("shop")]
        public async Task ShopCommand(CommandContext ctx)
        {
            // Retrieve shop from SQL server
            Shop s = new Shop();
            s.GetShop();
            ShopEmbed embed = new ShopEmbed(s, ctx);

            await ctx.RespondAsync("", embed.builder.Build());
        }
    }
}
