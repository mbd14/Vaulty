using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Utils;

namespace Vaulty.Embeds.Help
{
    public class HelpAllEmbed : VaultyEmbed
    {
        public HelpAllEmbed(CommandContext ctx) 
        {
            var modules = new List<Tuple<string, string, bool>>
            {
                new Tuple<string, string, bool>(
                    "Economy",
                    "Commands for managing balance, bank, and payments.",
                    false
                ),
                new Tuple<string, string, bool>(
                    "Rewards",
                    "Daily and weekly rewards with streak bonuses.",
                    false
                ),
                new Tuple<string, string, bool>(
                    "Jobs",
                    "Work-based earnings with tiers and XP progression.",
                    false
                ),
                new Tuple<string, string, bool>(
                    "Shop",
                    "Buy and manage virtual items (coming soon).",
                    false
                ),
                new Tuple<string, string, bool>(
                    "Profile",
                    "View your XP level, job, and inventory.",
                    false
                )
            };

            BuildBasicEmbed
            (
                ctx: ctx,
                title: "📦 Available Bot Modules",
                fields: modules,
                color: DiscordColor.Gold
            );


        }
    }
}
