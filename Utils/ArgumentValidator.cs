using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;

namespace Vaulty.Utils
{
    public class ArgumentValidator
    {
        public static bool DepositCheck(CommandContext ctx, string[] args)
        {
            int x = 0;
            if 
                (
                args.Length != 1 ||                     // Check if args amount is right
                !int.TryParse(args[0], out x)           // Check type or argument 1 : coins
                )
            {
                VaultyEmbed vaultyEmbed = new VaultyEmbed();

                vaultyEmbed.BuildBasicEmbed
                (
                    ctx: ctx,
                    description: Const.USAGE_DEPOSIT,
                    color: DiscordColor.Red
                );

                ctx.RespondAsync("", vaultyEmbed.builder.Build());
                return false;
            }
            return true;
        }

        public static bool WithdrawCheck(CommandContext ctx, string[] args) 
        {
            int x = 0;
            if 
                (
                args.Length != 1 ||                     // Check if args amount is right
                !int.TryParse(args[0], out x)           // Check type or argument 1 : coins
                )
            {
                VaultyEmbed vaultyEmbed = new VaultyEmbed();

                vaultyEmbed.BuildBasicEmbed
                (
                    ctx: ctx,
                    description: Const.USAGE_WITHDRAW,
                    color: DiscordColor.Red
                );

                ctx.RespondAsync("", vaultyEmbed.builder.Build());
                return false;
            }

            return true;
        }

        public static bool PayCheck(CommandContext ctx, params string[] args)
        {
            if(args.Length == 0)
            {
                ctx.RespondAsync(Const.USAGE_PAY);
                return false;
            }
            ulong userId = ulong.Parse(args[0].Trim('<', '@', '>'));
            int x;
            if 
                (
                args.Length != 2||
                !int.TryParse(args[1], out x) ||
                ctx.Guild.GetMemberAsync(userId) == null
                )
            {
                VaultyEmbed vaultyEmbed = new VaultyEmbed();

                vaultyEmbed.BuildBasicEmbed
                (
                    ctx: ctx,
                    description: Const.USAGE_PAY,
                    color: DiscordColor.Red
                );

                ctx.RespondAsync("", vaultyEmbed.builder.Build());
                return false;
            }
            if (args[0] == ctx.User.Id.ToString())
            {
                VaultyEmbed vaultyEmbed = new VaultyEmbed();

                vaultyEmbed.BuildBasicEmbed
                (
                    ctx: ctx,
                    description: "Tu ne peux pas te payer toi même",
                    color: DiscordColor.Red
                );

                ctx.RespondAsync("", vaultyEmbed.builder.Build());
                return false;
            }
            return true;
        }

    }
}
