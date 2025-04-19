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
    public class WorkExperienceEmbed : VaultyEmbed
    {
        public WorkExperienceEmbed(CommandContext ctx, string jobName, int level, int currentXp, int xpToNextLevel, Job j)
        {
            double xpPercent = (double)currentXp / xpToNextLevel * 100;
            string xpBar = GenerateProgressBar(xpPercent);

            BuildBasicEmbed
            (
                ctx: ctx,
                title: $"{ctx.User.Username} - Expérience de travail",
                fields: new List<Tuple<string, string, bool>>
                {
                        // First line : Xp summary block
                        new Tuple<string, string, bool>
                        (
                            "Niveau Actuelㅤㅤㅤ",
                            $"`{level}`",
                            true
                        ),
                        new Tuple<string, string, bool>
                        (
                            "XPㅤㅤㅤㅤㅤㅤㅤㅤㅤ",
                            $"`{currentXp} / {xpToNextLevel}`",
                            true
                        ),
                        new Tuple<string, string, bool>
                        (
                            "Progressionㅤㅤㅤㅤ",
                            xpBar,
                            true
                        ),
                        // Second line : Salary and job summary
                        new Tuple<string, string, bool>
                        (
                            "Jobㅤㅤㅤㅤㅤㅤㅤㅤ",
                            jobName,
                            true
                        ),
                        new Tuple<string, string, bool>
                        (
                            "Salaireㅤㅤㅤㅤㅤㅤ",
                            $"{j.SalaryMin} - {j.SalaryMax} {Const.VAULTYCOINS_EMOJI}",
                            true
                        ),
                        new Tuple<string, string, bool>
                        (
                            "Gain d'experience",
                            $"{j.WorkXp} XP",
                            true
                        )
                },
                color: DiscordColor.Orange
            );
        }

        private string GenerateProgressBar(double percent)
        {
            int totalBars = 10;
            int filledBars = (int)Math.Floor((percent / 100) * totalBars);
            int emptyBars = totalBars - filledBars;

            return $"[`{new string('■', filledBars)}{new string('▫', emptyBars)}`] `{percent:0.#}%`";
        }
    }
}
