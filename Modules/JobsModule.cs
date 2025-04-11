using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Embeds;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    [Command("job")]
    public class JobsModule
    {

        [Command("list")]
        public async Task ListJobs(CommandContext ctx)
        {
            await ctx.RespondAsync("Here are the available jobs...");
        }

        [Command("apply")]
        public async Task ApplyJob(CommandContext ctx, string jobName)
        {
            await ctx.RespondAsync($"You applied for the job: {jobName}");
        }

        [Command("quit")]
        public async Task QuitJob(CommandContext ctx)
        {
            await ctx.RespondAsync("You quit your job.");
        }

        [Command("work")]
        public async Task WorkJob(CommandContext ctx)
        {
            //await ctx.RespondAsync("You did your job and earned coins!");

            // Retrieve user and executions of user
            User u = new User() { Id = ctx.User.Id.ToString() };
            CommandExecutions executions = new CommandExecutions() { Id = ctx.User.Id.ToString() };
            u.ReadUser();
            executions.GetExecution();

            ResponseEmbed embed;

            string dateTimeOffset = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            ulong elapsed = ulong.Parse(dateTimeOffset) - ulong.Parse(executions.LastWork);

            // If the user already redeemed their daily reward for that day
            if (elapsed < Const.SECONDS_IN_HOUR)
            {
                TimeSpan remaining = TimeSpan.FromSeconds(Const.SECONDS_IN_HOUR - elapsed);
                embed = new ResponseEmbed(ctx, $"Vous avez déjà travaillé. Vous pouvez relancer la commande à cette date : <t:{ulong.Parse(executions.LastWork) + Const.SECONDS_IN_HOUR}:F>.", col: DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            // Change data models in controler
            Random n = new Random(int.Parse(dateTimeOffset));
            int reward = n.Next(Const.WORK_REWARD, Const.WORK_REWARD * 2) ;
            u.VaultCoins += reward;
            executions.LastWork = dateTimeOffset;

            //Update db
            u.ModifyUser();
            executions.ModifyExecution();

            // Send answer
            embed = new ResponseEmbed(ctx, string.Format("Vous avez recu votre salaire de {0} {1}. Revenez dans 1 heure pour récupérer votre prochain salaire.", reward, Const.VAULTYCOINS_EMOJI), col: DiscordColor.Green);
            await ctx.RespondAsync(embed.builder.Build());

        }
    }
}
