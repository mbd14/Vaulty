
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Vaulty.Database;
using Vaulty.Database.Models;
using Vaulty.Utils;

namespace Vaulty.Modules
{
    public class UserModule : ApplicationCommandModule
    {
        /// <summary>
        /// Add a user to the Vaulty data base.
        /// This command is used for debuging purpouses.
        /// This won't be an available command on the release
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("user-add")]
        public async Task User_Add(CommandContext ctx)
        {
            User u = new User { Id = (ctx.User.Id.ToString()), VaultCoins = Const.USER_ADD_DEFAULT_COINS, Vaultium = Const.USER_ADD_DEFAULT_VAULTIUM };
            u.InsertUser();
            await ctx.RespondAsync("User has been added");
        }

        /// <summary>
        /// Show a contextual window with the user's info
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("user-show")]
        public async Task User_Show(CommandContext ctx)
        {
            User u = new User() { Id = ctx.User.Id.ToString() };
            u.ReadUser();
            await ctx.RespondAsync(string.Format("user <@{0}> | coins {1}", u.Id, u.VaultCoins));
        }

        [Command("user-add-all")]
        public async Task AddAll(CommandContext ctx)
        {
            if (ctx.User.Id.ToString() != "460806719682117632")
            {
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();

                embedBuilder.WithColor(DiscordColor.Red);
                embedBuilder.WithDescription("Sensitive command : Directly adds every guild members to the SQL Server DB. Due to it's overly sensitive nature, the command is only executable by <@460806719682117632>");

                await ctx.RespondAsync(embedBuilder.Build());

                return;
            }

            var guild = await ctx.Client.GetGuildAsync(ctx.Guild.Id);

            var members = ctx.Guild.GetAllMembersAsync();
            int i = 0;
            await foreach (var member in members)
            {
                User u = new User { Id = (member.Id.ToString()), VaultCoins = Const.USER_ADD_DEFAULT_COINS, Vaultium = Const.USER_ADD_DEFAULT_VAULTIUM };
                u.InsertUser();
                i++;
            }

            await ctx.RespondAsync(i + " users inserted. Check localhost,1433 for confirmation");
        }

    }
}
