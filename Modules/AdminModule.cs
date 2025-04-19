using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Embeds;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    /// <summary>
    /// The Admin module holds all the commands that are executable only by users with admin privileges
    /// </summary>
    public class AdminModule
    {
        public AdminModule() { }

        #region Command : Give
        [Command("give-coins")]
        public async Task GiveCommand(CommandContext ctx, DiscordUser usr, int amount)
        {
            ResponseEmbed reponse;
            if (!ctx.Member.Permissions.HasPermission(DiscordPermission.Administrator))
            {
                reponse = new ResponseEmbed
                    (
                    ctx,
                    string.Format("Vous ne pouvez pas faire ca",
                    usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                    DiscordColor.Red
                    );
                await ctx.RespondAsync("", reponse.builder.Build());
                return;
            }

            string[] args = [usr.Id.ToString(), amount.ToString()];
            if (!ArgumentValidator.PayCheck(ctx, args)) return;

            User payed_u = new User() { Id = usr.Id.ToString() };

            payed_u.ReadUser();
            payed_u.VaultCoins += amount;
            payed_u.ModifyUser();

            reponse = new ResponseEmbed
                (
                ctx,
                string.Format("Vous avez payé {0} {1} {2}",
                usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                DiscordColor.Green
                );

            await ctx.RespondAsync("", reponse.builder.Build());
        }
        #endregion

        #region Command : Remove
        [Command("take-coins")]
        public async Task TakeCommand(CommandContext ctx, DiscordUser usr, int amount)
        {
            ResponseEmbed reponse;
            if (!ctx.Member.Permissions.HasPermission(DiscordPermission.Administrator))
            {
                reponse = new ResponseEmbed
                    (
                    ctx,
                    string.Format("Vous ne pouvez pas faire ca",
                    usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                    DiscordColor.Red
                    );
                await ctx.RespondAsync("", reponse.builder.Build());
                return;
            }

            string[] args = [usr.Id.ToString(), amount.ToString()];
            if (!ArgumentValidator.PayCheck(ctx, args)) return;

            User payed_u = new User() { Id = usr.Id.ToString() };

            payed_u.ReadUser();
            payed_u.VaultCoins -= amount;
            // Safeguard for negative amounts
            payed_u.VaultCoins  = payed_u.VaultCoins < 0 ? 0 : payed_u.VaultCoins;
            payed_u.ModifyUser();

            reponse = new ResponseEmbed
                (
                ctx,
                string.Format("Vous avez retiré à {0} {1} {2}",
                usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                DiscordColor.Green
                );

            await ctx.RespondAsync("", reponse.builder.Build());
        }
        #endregion

        #region Command : Remove
        [Command("set-coins")]
        public async Task SetCommand(CommandContext ctx, DiscordUser usr, int amount)
        {
            ResponseEmbed reponse;
            if (!ctx.Member.Permissions.HasPermission(DiscordPermission.Administrator))
            {
                reponse = new ResponseEmbed
                    (
                    ctx,
                    string.Format("Vous ne pouvez pas faire ca",
                    usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                    DiscordColor.Red
                    );
                await ctx.RespondAsync("", reponse.builder.Build());
                return;
            }

            string[] args = [usr.Id.ToString(), amount.ToString()];
            if (!ArgumentValidator.PayCheck(ctx, args)) return;

            User payed_u = new User() { Id = usr.Id.ToString() };

            payed_u.ReadUser();
            payed_u.VaultCoins = amount;
            payed_u.ModifyUser();

            reponse = new ResponseEmbed
                (
                ctx,
                string.Format("{0} possède maintenant {1} {2}",
                usr.Mention, amount, Const.VAULTYCOINS_EMOJI),
                DiscordColor.Green
                );

            await ctx.RespondAsync("", reponse.builder.Build());
        }
        #endregion
    }
}
