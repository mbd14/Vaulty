using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using Vaulty.Database.Models;
using Vaulty.Database;

namespace Vaulty.Modules
{
    public class UserModule : ApplicationCommandsModule
    {

        [SlashCommand("user-add", "Add user to vaulty")]
        public async Task Slash_User_Add(InteractionContext ctx)
        {
            User u = new User { Id = (ctx.User.Id.ToString()), Coins = 10000};
            DbCon db = new DbCon();
            User.InsertUser(u, db);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            {
                Content = "Added user "
            });
        }

        [SlashCommand("user-show", "Show a user")]
        public async Task Slash_User_Show(InteractionContext ctx)
        {
            DbCon db = new DbCon();
            User u = new User();
            u.ReadUser(db);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            {
                Content = string.Format("user {0} | coins {1}", u.Id, u.Coins)
            });
        }

    }
}
