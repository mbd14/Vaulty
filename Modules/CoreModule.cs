using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.Entities;
using Vaulty.Database;
using Vaulty.Database.Models;
using Vaulty.Embeds;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    /// <summary>
    /// The Core module holds all the core mechanics of the economy system
    /// </summary>
    public class CoreModule
    {
        public CoreModule() { }

        #region Command : Balance
        /// <summary>
        /// The balance command allows the user to see their VaultCoins balance
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("balance")]
        [TextAlias("bal")]
        public async Task BalanceCommand(CommandContext ctx)
        {
            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString()};
            u.ReadUser();
            
            // Construct embed to display user balance
            BalanceEmbed embed = new BalanceEmbed(ctx, u);  

            // Build and send the embed
            await ctx.RespondAsync("", embed.builder.Build());
        }
        #endregion

        #region Command : Deposit
        /// <summary>
        /// The deposit command allows the user to deposit a certain amount of VaultCoins into their Bank.
        /// The amount of VaultCoins is specified as an argument in the command
        /// The amount cannot exceed the amount of coins the user has
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [Command("deposit")]
        public async Task DepositCommand(CommandContext ctx, int depositAmount)
        {
            string[] args = [depositAmount.ToString()];
            if (!ArgumentValidator.DepositCheck(ctx, args)) return;


            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString()};
            u.ReadUser();

            // Modify values in Bank and Wallet
            u.Bank += depositAmount;
            u.VaultCoins -= depositAmount;

            // Modify the user 
            u.ModifyUser();

            ResponseEmbed reponse = new ResponseEmbed(ctx, "Vous avez déposé votre argent", DiscordColor.Green);
            await ctx.RespondAsync("", reponse.builder.Build());

        }
        #endregion

        #region Command : Withdraw
        /// <summary>
        /// The withdraw command allows the user to withdraw a certain amount of VaultCoins from their Bank into their wallet.
        /// The amount of VaultCoins is specified as an argument in the command
        /// The amount cannot exceed the amount of coins the user has in their bank
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>

        [Command("withdraw")]
        public async Task WithdrawCommand(CommandContext ctx, int amount)
        {
            string[] args = [amount.ToString()];
            if (!ArgumentValidator.WithdrawCheck(ctx, args)) return;

            // Retrieve arguments
            int withdrawAmount = int.Parse(args[0]);

            // Retrieve user that executed the command
            User u = new User() { Id = ctx.User.Id.ToString() };
            u.ReadUser();

            // Modify values in Bank and Wallet
            u.Bank -= withdrawAmount;
            u.VaultCoins += withdrawAmount;

            // Modify the user 
            u.ModifyUser();

            ResponseEmbed reponse = new ResponseEmbed(ctx, "Vous avez retiré votre argent", DiscordColor.Green);
            await ctx.RespondAsync("", reponse.builder.Build());

        }


        #endregion

        #region Command : Pay
        /// <summary>
        /// The pay command allows a user to pay a certain amount of money to another user.
        /// The first argument is the user you wish to pay. Can be anyone but yourself
        /// The second argument is the amount of money. Amount shouldn't be more than what you own
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="usr"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [Command("pay")]
        public async Task PayCommand(CommandContext ctx, DiscordUser usr, int amount)
        {
            string[] args =  [usr.Id.ToString(), amount.ToString()];
            if (!ArgumentValidator.PayCheck(ctx, args)) return;


            // Retrieve PayingUser and Paid User
            User paying_u = new User() { Id = ctx.User.Id.ToString() };
            User payed_u  = new User() { Id = usr.Id.ToString() };

            paying_u.ReadUser();
            payed_u.ReadUser();

            // Update balance
            paying_u.VaultCoins -= amount;
            payed_u.VaultCoins += amount;

            paying_u.ModifyUser();
            payed_u.ModifyUser();


            ResponseEmbed reponse = new ResponseEmbed(ctx, string.Format("Vous avez payé {0} {1} VaultCoins", usr.Mention, amount), DiscordColor.Green);
            await ctx.RespondAsync("", reponse.builder.Build());
        }
        #endregion
    }
}
