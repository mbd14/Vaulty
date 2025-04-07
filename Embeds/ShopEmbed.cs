using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Trees;
using DSharpPlus.Entities;
using Vaulty.Database.Models;
using Vaulty.Utils;

namespace Vaulty.Embeds
{
    /// <summary>
    /// Represents an embed that displays the shop. Inherits from the generic VaultyEmbed
    /// </summary>
    public class ShopEmbed : VaultyEmbed
    {
        public ShopEmbed(Shop s, CommandContext ctx) 
        {
            List<Tuple<string, string, bool>> t_items = new List<Tuple<string, string, bool>>();
            foreach (Item i in s.Items)
            {
                t_items.Add(new Tuple<string, string, bool>(string.Format("{0} - {1}", i.Label, i.Price.ToString()), i.Description, false));
            }

            BuildBasicEmbed
            (
                ctx: ctx,
                title: "Shop",
                fields: t_items,
                color: DiscordColor.SpringGreen
            );
        }
    }
}
