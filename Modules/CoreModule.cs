using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Database;
using Vaulty.Database.Models;

namespace Vaulty.Modules
{
    /// <summary>
    /// The Core module holds all the core mechanics of the economy system
    /// </summary>
    public class CoreModule
    {
        public CoreModule() { }

        /// <summary>
        /// The balance command allows the user to see their VaultCoins balance
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("balance")]
        public async Task BalanceCommand(CommandContext ctx)
        {
            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString()};
            u.ReadUser();

            // Prepare the EmbedBuilder to display balance information
            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();

            embedBuilder.Title = string.Format("{0} balance", ctx.User.GlobalName);
            embedBuilder.WithDescription("Récapitulatif de vos comptes");
            embedBuilder.AddField("VaultCoins\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc", ":coin: " + u.VaultCoins.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'), true);
            embedBuilder.AddField("Vaultium  \u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc", ":yen: "  + u.Vaultium.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'), true);
            embedBuilder.AddField("Bank      \u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc", ":bank: " + u.Bank.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'), true);

            // Build and send the embed
            await ctx.RespondAsync("", embedBuilder.Build());
        }


        /// <summary>
        /// The deposit command allows the user to deposit a certain amount of VaultCoins into their Bank.
        /// The amount of VaultCoins is specified as an argument in the command
        /// The amount cannot exceed the amount of coins the user has
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [Command("deposit")]
        public async Task DepositCommand(CommandContext ctx, params string[] args)
        {

            int x;
            if (args.Length != 1 || !int.TryParse(args[0], out x))
            {
                await ctx.RespondAsync("Usage : v!deposit <coins> ");
                return;
            }

            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString()};
            u.ReadUser();

            // Modify values in Bank and Wallet
            u.Bank += x;
            u.VaultCoins -= x;

            // Modify the user 
            u.ModifyUser();

            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();

            embedBuilder.WithDescription("Vous avez déposé votre argent");
            embedBuilder.WithColor(DiscordColor.Green);

            await ctx.RespondAsync("", embedBuilder.Build());

        }

        /// <summary>
        /// The withdraw command allows the user to withdraw a certain amount of VaultCoins from their Bank into their wallet.
        /// The amount of VaultCoins is specified as an argument in the command
        /// The amount cannot exceed the amount of coins the user has in their bank
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("withdraw")]
        public async Task WithdrawCommand(CommandContext ctx, params string[] args)
        {

            int x;
            if (args.Length != 1 || !int.TryParse(args[0], out x))
            {
                await ctx.RespondAsync("Usage : v!withdraw <coins> ");
                return;
            }

            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString()};
            u.ReadUser();

            // Modify values in Bank and Wallet
            u.Bank -= x;
            u.VaultCoins += x;


            // Modify the user 
            u.ModifyUser();

            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();

            embedBuilder.WithDescription("Vous avez retiré votre argent");
            embedBuilder.WithColor(DiscordColor.Green);

            await ctx.RespondAsync("", embedBuilder.Build());

        }
    }
}
