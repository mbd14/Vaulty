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
            JobListEmbed embed;

            List<Job> jobs = Job.GetJobList();

            embed = new JobListEmbed(ctx, jobs);

            await ctx.RespondAsync(embed.builder.Build());
        }

        [Command("apply")]
        public async Task ApplyJob(CommandContext ctx, int jobId)
        {
            ResponseEmbed embed;

            Job j = new Job();

            j.GetJob(jobId);

            if(j.Id == -1)
            {
                embed = new ResponseEmbed(ctx, "Ce metier n'existe pas", DiscordColor.Red);
                await ctx.RespondAsync(embed.builder.Build());
                return;
            }

            User u = new User() { Id = ctx.User.Id.ToString() };
            u.ReadUser();
            u.Job = jobId;
            u.ModifyUser();

            embed = new ResponseEmbed(ctx, $"Vous exercez mainetant le metier : **{j.Label}**", DiscordColor.Green);
            await ctx.RespondAsync(embed.builder.Build());
            return;


        }

        [Command("quit")]
        public async Task QuitJob(CommandContext ctx)
        {
            await ctx.RespondAsync("You quit your job.");
        }

        [Command("work")]
        public async Task WorkJob(CommandContext ctx)
        {
            ResponseEmbed embed;

            // Retrieve user
            User u = new User() { Id = ctx.User.Id.ToString() };
            u.ReadUser();

            // Retrieve user executions
            CommandExecutions executions = new CommandExecutions() { Id = ctx.User.Id.ToString() };
            executions.GetExecution();

            // Retrieve user job
            Job j = new Job();
            j.GetJob(u.Job);

            //Retrieve experience 
            WorkExperience wexp = new WorkExperience(u.Id);
            wexp.GetWorkExperience();

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
            int reward = n.Next(j.SalaryMin, j.SalaryMax) ;
            u.VaultCoins += reward;
            executions.LastWork = dateTimeOffset;
            wexp.WorkXp += j.WorkXp;
            if(wexp.WorkXp >= wexp.XpUntilNextLevel)
            {
                wexp.WorkLevel++;
                wexp.XpUntilNextLevel = WorkExperience.CalculateXpForNextLevel(wexp.WorkLevel);
            }

            //Update db
            u.ModifyUser();
            executions.ModifyExecution();
            wexp.UpdateWorkExperience();

            // Send answer
            embed = new ResponseEmbed(ctx, string.Format("Vous avez recu votre salaire de {0} {1} pour votre travail en tant que : **{2}**. Revenez dans 1 heure pour récupérer votre prochain salaire.", reward, Const.VAULTYCOINS_EMOJI, j.Label), col: DiscordColor.Green);
            await ctx.RespondAsync(embed.builder.Build());

        }

        [Command("info")]
        public async Task InfoJob(CommandContext ctx)
        {
            WorkExperienceEmbed embed;

            // Retrieve user
            User u = new User() { Id = ctx.User.Id.ToString() };
            u.ReadUser();

            // Retrieve user job
            Job j = new Job();
            j.GetJob(u.Job);
            WorkExperience w = new WorkExperience(u.Id);
            w.GetWorkExperience();

            embed = new WorkExperienceEmbed(ctx, j.Label, w.WorkLevel, w.WorkXp, w.XpUntilNextLevel, j);
            await ctx.RespondAsync(embed.builder.Build());
        }
    }

    public class LegacyJobModule()
    {
        [Command("work")]
        public async Task workCommandLegacy(CommandContext ctx)
        {
            JobsModule module = new JobsModule();
            await module.WorkJob(ctx);
        }
    }
}
