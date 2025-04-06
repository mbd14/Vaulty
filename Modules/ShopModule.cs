using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Vaulty.Database;
using Vaulty.Database.Models;


namespace Vaulty.Modules
{
    public class ShopModule
    {
        [Command("shop")]
        public async Task ShopCommand(CommandContext ctx)
        {
            // Retrieve shop from SQL server
            Shop s = new Shop();
            s.GetShop();

            // Build embed message for Shop display
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.SpringGreen,
                Description = "That's the shop",
                Title = "Shop",
            };
            // Add field for each item
            foreach(Item i in s.Items)
            {
                embed.AddField(string.Format("{0} - {1}", i.Label, i.Price.ToString()), i.Description);
            }

            // Send the embed with the shop
            await ctx.RespondAsync("", embed.Build());
        }
    }
}
