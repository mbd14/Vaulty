using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using Vaulty.Embeds;
using Vaulty.Embeds.Help;

namespace Vaulty.Modules
{
    [Command("help")]
    public class HelpModule
    {

        public HelpModule() { }

        [Command("all")]
        public async Task HelpAllCommand(CommandContext ctx)
        {
            HelpAllEmbed embed = new HelpAllEmbed(ctx);

            ctx.RespondAsync(embed.builder.Build());

        }
    }
}
