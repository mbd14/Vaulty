using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using Vaulty.Utils;

namespace Vaulty.Embeds
{
    public class ResponseEmbed : VaultyEmbed
    {
        public ResponseEmbed(CommandContext ctx, string desc, DiscordColor col) 
        {
            BuildBasicEmbed
            (
                ctx: ctx,
                description: desc,
                color: col
            );
        }
    }
}
