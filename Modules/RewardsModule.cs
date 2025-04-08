using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Embeds;
using Vaulty.Utils;
using CommandAttribute = DSharpPlus.Commands.CommandAttribute;

namespace Vaulty.Modules
{
    /// <summary>
    /// Rewards module holds the commands that give a definitive reward to the user
    /// </summary>
    public class RewardsModule
    {
        public RewardsModule() { }

        #region Command : Daily
        /// <summary>
        /// Gives the user their daily VaultyCoins reward.
        /// </summary>
        [Command("daily")]
        public async Task DailyCommand(CommandContext ctx)
        {
            
            // Retrieve user and executions of user
            User u = new User() { Id = ctx.User.Id.ToString() };
            CommandExecutions executions = new CommandExecutions( ) { Id = ctx.User.Id.ToString() };
            u.ReadUser();
            executions.GetExecution();

            ResponseEmbed embed;
            
            string dateTimeOffset = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            ulong elapsed = ulong.Parse(dateTimeOffset) - ulong.Parse(executions.LastDaily);

            // If the user already redeemed their daily reward for that day
            if (elapsed < Const.SECONDS_IN_DAY)
            {
                TimeSpan remaining = TimeSpan.FromSeconds(Const.SECONDS_IN_DAY - elapsed);
                embed = new ResponseEmbed(ctx, $"Vous avez déjà récupéré vos récompenses du jour. Vous pouvez relancer la commande à cette date : <t:{ulong.Parse(executions.LastDaily) + Const.SECONDS_IN_DAY}:F>.", col: DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            // Change data models in controler
            int reward = Const.DAILY_REWARD;
            u.VaultCoins += reward;
            executions.LastDaily = dateTimeOffset;

            //Update db
            u.ModifyUser();
            executions.ModifyExecution();

            // Send answer
            embed = new ResponseEmbed(ctx, string.Format("Vous avez recu vos {0} {1} journalier.", reward, Const.VAULTYCOINS_EMOJI), col: DiscordColor.Green);
            await ctx.RespondAsync(embed.builder.Build());

        }
        #endregion

        #region Command : Weekly
        /// <summary>
        /// Gives the user their weekly reward.
        /// </summary>
        [Command("weekly")]
        public async Task WeeklyCommand(CommandContext ctx)
        {
            User u = new User() { Id = ctx.User.Id.ToString() };
            CommandExecutions executions = new CommandExecutions() { Id = ctx.User.Id.ToString() };
            u.ReadUser();
            executions.GetExecution();

            ResponseEmbed embed;

            string dateTimeOffset = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            ulong elapsed = ulong.Parse(dateTimeOffset) - ulong.Parse(executions.LastWeekly);

            // If the user already redeemed their daily reward for that day
            if (elapsed < Const.SECONDS_IN_WEEK)
            {
                TimeSpan remaining = TimeSpan.FromSeconds(Const.SECONDS_IN_WEEK - elapsed);
                embed = new ResponseEmbed(ctx, $"Vous avez déjà récupéré vos récompenses de la semaine. Vous pouvez relancer la commande à cette date : <t:{ulong.Parse(executions.LastDaily) + Const.SECONDS_IN_DAY}:F>.", col: DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            int reward = Const.WEEKLY_REWARD;
            u.VaultCoins += reward;
            executions.LastWeekly = dateTimeOffset;

            //Update db
            u.ModifyUser();
            executions.ModifyExecution();

            // Send answer
            embed = new ResponseEmbed(ctx, string.Format("Vous avez recu vos {0} {1} hebdomadaires.", reward, Const.VAULTYCOINS_EMOJI), col: DiscordColor.Green);
            await ctx.RespondAsync(embed.builder.Build());
        }
        #endregion

        #region Command : Beg
        /// <summary>
        /// Gives the user a chance to earn a small amount of coins randomly.
        /// </summary>
        [Command("beg")]
        public async Task BegCommand(CommandContext ctx)
        {
        }
        #endregion

        #region Command : Quest
        /// <summary>
        /// Allows the user to complete a quest for a reward.
        /// </summary>
        [Command("quest")]
        public async Task QuestCommand(CommandContext ctx)
        {
        }
        #endregion

        #region Command : Crime
        /// <summary>
        /// Gives the user a chance to commit a crime for coins, with a chance to fail.
        /// </summary>
        [Command("crime")]
        public async Task CrimeCommand(CommandContext ctx)
        {
        }
        #endregion
    }
}
