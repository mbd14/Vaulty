using System.Reflection;
using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using Microsoft.Extensions.Logging;
using Vaulty.Modules;
using Vaulty.Utils;
using static Vaulty.Modules.UserModule;

namespace Vaulty.App
{
    public class Vaulty
    {
        public DiscordClient _client;

        /// <summary>
        /// Configurate the Discord Client
        /// </summary>
        public Vaulty()
        {
            _client = new DiscordClient(new DiscordConfiguration()
            {
                Token = JsonSensitiveLoader.TokenLoad(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContent,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            });

        }

        /// <summary>
        /// Start the discord client
        /// </summary>
        public async void Start()
        {
            await _client.ConnectAsync();
            RegisterSlashCommands();
        }

        /// <summary>
        /// Registers every slash command to the client
        /// </summary>
        public void RegisterSlashCommands()
        {
            _client.UseApplicationCommands().RegisterGlobalCommands(Assembly.GetAssembly(typeof(UserModule)));

        }
    }
}
