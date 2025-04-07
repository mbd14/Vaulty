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
        /// <summary>
        /// Represents an embed that displays a generic reponse to a command.
        /// Usually used to confirm a command was executed or to display an error message
        /// </summary>
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
