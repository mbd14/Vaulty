using System.Data;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Embeds;
using Vaulty.Utils;
using static Vaulty.Database.Models.Inventory;


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

        [Command("buy")]
        public async Task BuyCommand(CommandContext ctx, [RemainingText] string item)
        {
            ResponseEmbed embed;
            // Get shop and user 
            Shop s = new Shop();
            User u = new User() { Id = ctx.Member.Id.ToString()};
            u.ReadUser();
            s.GetShop();

            // Get user inventory
            Inventory userInventory = new Inventory();
            userInventory.GetUserInventory(u.Id);
            
            // Item in the shop the user wants to buy
            Item i = s.Items.FirstOrDefault(x => x.Label == item);

            // Item isnt in the shop
            if(i == null)
            {
                embed = new ResponseEmbed(ctx, $"L'objet \"**{item}**\" n'est pas dans la boutique.", col: DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            // Not enough money to buy
            if(i.Price > u.VaultCoins)
            {
                embed = new ResponseEmbed(ctx, $"Vous n'avez pas les fonds suffisant pour acheter **{item}", col: DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            u.VaultCoins -= i.Price;
            Inventoryitem itemBought = userInventory.inventoryitems.FirstOrDefault(x => x.ItemId == i.Id);

            // Item wasn't in the inventory
            if (itemBought == null)
            {
                userInventory.AddItemToInventory(u.Id, i);
            }
            // Item was in the inventory
            else
            {
                // If it's a role, can only be given once
                if (i.ItemType == 0 && itemBought.Quantity>0)
                {
                    embed = new ResponseEmbed(ctx, $"Vous avez déjà acheté **{i.Label}**", DiscordColor.Red);
                    await ctx.RespondAsync("", embed.builder.Build());
                    return;
                }

                // Increment the amount of items owned in the inventory
                userInventory.AddItemToInventoryIncrement(u.Id, i);
            }

            u.ModifyUser();

            if(i.ItemType == 0)
            {
                GiveRole(u.Id, i.Id, ctx);
            }

            embed = new ResponseEmbed(ctx, $"Vous avez acheté **{i.Label}** pour **{i.Price}** {Const.VAULTYCOINS_EMOJI}", DiscordColor.Green);
            embed.builder.Build();
            await ctx.RespondAsync("", embed.builder.Build());
        }

        public void GiveRole(string uid, int iid, CommandContext ctx)
        {
            var member = ctx.Guild.GetMemberAsync(ulong.Parse(uid));
            switch (iid)
            {
                case 2:
                    member.Result.GrantRoleAsync(ctx.Guild.GetRoleAsync(1323795368315719722).Result);
                    break;
                case 3:
                    member.Result.GrantRoleAsync(ctx.Guild.GetRoleAsync(1290704451140718694).Result);
                    break;
                case 4:
                    member.Result.GrantRoleAsync(ctx.Guild.GetRoleAsync(1323644584928084038).Result);
                    break;
            }
        }
    }
}
