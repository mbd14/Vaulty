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
    public class CooldownEmbed : VaultyEmbed
    {
        public CooldownEmbed(CommandContext ctx, User u, CommandExecutions c) 
        {

            BuildBasicEmbed
            (
                ctx: ctx,
                title: "Command cooldowns",
                fields: new List<Tuple<string, string, bool>>
                {
                    new Tuple<string, string, bool>
                    (
                        "Work",
                         workIsAvailable(c.LastWork) ?
                         $"✅ Disponible depuis <t:{ulong.Parse(c.LastWork) + Const.SECONDS_IN_HOUR}:R>" :
                         $"❌ Disponible dans   <t:{(ulong.Parse(c.LastWork) + Const.SECONDS_IN_HOUR)}:R>",
                        false
                    ),
                    new Tuple<string, string, bool>
                    (
                        "Daily",
                         dailyIsAvailable(c.LastDaily) ?
                         $"✅ Disponible depuis <t:{ulong.Parse(c.LastDaily) + Const.SECONDS_IN_DAY}:R>" :
                         $"❌ Disponible dans   <t:{(ulong.Parse(c.LastDaily) + Const.SECONDS_IN_DAY)}:R>",
                        false
                    ),
                    new Tuple<string, string, bool>
                    (
                        "Weekly",
                         weeklyIsAvailable(c.LastWeekly) ?
                         $"✅ Disponible depuis <t:{ulong.Parse(c.LastWeekly) + Const.SECONDS_IN_WEEK}:R>" :
                         $"❌ Disponible dans   <t:{(ulong.Parse(c.LastWeekly) + Const.SECONDS_IN_WEEK)}:R>",
                        false
                    ),

                },
                DiscordColor.Green
            );
        }

        public bool workIsAvailable(string epoch)
        {
            return (ulong) (DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(epoch)) > Const.SECONDS_IN_HOUR;
        }

        public bool dailyIsAvailable(string epoch)
        {
            return (ulong)(DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(epoch)) > Const.SECONDS_IN_DAY;
        }

        public bool weeklyIsAvailable(string epoch)
        {
            return (ulong)(DateTimeOffset.Now.ToUnixTimeSeconds() - long.Parse(epoch)) > Const.SECONDS_IN_WEEK;
        }
    }

}
