using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Utils;

namespace Vaulty.Embeds
{
    public class JobListEmbed : VaultyEmbed
    {
        public JobListEmbed(CommandContext ctx, List<Job> jobs) 
        {
            List<Tuple<string, string, bool>> jobFields = new List<Tuple<string, string, bool>>();
            foreach (var job in jobs)
            {
                jobFields.Add(new Tuple<string, string, bool>
                    (
                    item1: $"{job.Id} - {job.Label}",
                    item2: $"Salaire : {Const.VAULTYCOINS_EMOJI} {job.SalaryMin} - {Const.VAULTYCOINS_EMOJI} {job.SalaryMax}\nExperience : {job.WorkXp}",
                    item3: false
                    )
                ); 
            };
            BuildBasicEmbed
                (
                ctx: ctx,
                title: "Liste des metiers disponibles",
                fields: jobFields,
                color: DiscordColor.Green
                );
        }
    }
}
