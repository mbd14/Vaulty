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
        [Command("give")]
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
    }
}
