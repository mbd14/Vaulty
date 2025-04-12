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
    public class RichestEmbed : VaultyEmbed
    {
        public RichestEmbed(List<User> topUsers, CommandContext ctx)
        {
            List<Tuple<string, string, bool>> user_info = new List<Tuple<string, string, bool>>();
            int i = 1;
            foreach (User user in topUsers) 
            {
                string name;
                try
                {
                    name = App.Vaulty._client.GetUserAsync(ulong.Parse(user.Id)).Result.Username;
                }
                catch(Exception e) 
                {
                    name = "Unknown";
                }
                user_info.Add
                    (new Tuple<string, string, bool>
                    (
                        $"**{i} - {name}**",
                        $"{Const.VAULTYCOINS_EMOJI} {user.VaultCoins}",
                        false
                    ));
                i++;
            }

            BuildBasicEmbed
            (
                ctx: ctx,
                title: "Joueurs les plus riches",
                fields: user_info,
                color: DiscordColor.Azure
            );
        }
    }
}
