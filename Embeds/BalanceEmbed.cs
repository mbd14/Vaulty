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
    /// <summary>
    /// Represents an embed that displays the user balance.
    /// </summary>
    public class BalanceEmbed : VaultyEmbed
    {

        public BalanceEmbed(CommandContext ctx, User u) 
        {
            // Build the embed for balance
            BuildBasicEmbed
            (
                ctx: ctx,
                title: "Récapitulatif",
                fields: new List<Tuple<string, string, bool>>
                {
                new Tuple<string, string, bool>
                (
                    "VaultCoins\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc",
                    Const.VAULTYCOINS_EMOJI + u.VaultCoins.ToString(" #,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'),
                    true
                ),
                new Tuple<string, string, bool>
                (
                    "Vaultium  \u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc",
                    Const.VAULTIUM_EMOJI  + u.Vaultium.ToString(" #,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'),
                    true
                ),
                new Tuple<string, string, bool>
                (
                    "Bank      \u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc\u1cbc",
                    ":bank: " + u.Bank.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.'),
                    true
                )
                },
                color: DiscordColor.Aquamarine
            );
        }

    }
}
