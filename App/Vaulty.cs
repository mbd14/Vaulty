﻿using System.Reflection;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Commands.Processors.TextCommands.Parsing;
using Microsoft.Extensions.Logging;
using Vaulty.Database.Models;
using Vaulty.Modules;
using Vaulty.Utils;
using static Vaulty.Modules.UserModule;

namespace Vaulty.App
{
    /// <summary>
    /// Vaulty object.
    /// Root of every command call.
    /// Holds the discord client and its configuration
    /// </summary>
    public class Vaulty
    {
        public DiscordClientBuilder _builder;

        public static DiscordClient _client;

        public static int bonus = JsonSensitiveLoader.BotInfoLoad().bonus;

        /// <summary>
        /// Configurate the Discord Client
        /// </summary>
        public Vaulty()
        {
            _builder = DiscordClientBuilder.CreateDefault(JsonSensitiveLoader.TokenLoad(), DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents);
            _builder.SetLogLevel(LogLevel.Trace);
            // Setup the commands extension
            _builder.UseCommands((IServiceProvider serviceProvider, CommandsExtension extension) =>
            {
                // Adding command modules
                extension.AddCommands
                ([
                    typeof(InfoModule),
                    typeof(UserModule),
                    typeof(CoreModule),
                    typeof(ShopModule),
                    typeof(RewardsModule),
                    typeof(JobsModule),
                    typeof(LegacyJobModule),
                    typeof(AdminModule),
                    typeof(HelpModule),
                ]);

                TextCommandProcessor textCommandProcessor = new(new()
                {
                    PrefixResolver = new DefaultPrefixResolver(true, "v!", "&", "!").ResolvePrefixAsync,
                });

                // Add text commands with a custom prefix (?ping)
                extension.AddProcessor(textCommandProcessor);
            }, new CommandsConfiguration()
            {
                // The default value is true, however it's shown here for clarity
                RegisterDefaultCommandProcessors = true
            });

            _builder.ConfigureEventHandlers
                (
                    b => b.HandleMessageCreated(async (s, e) =>
                    {
                        User payed_u = new User() { Id = e.Author.Id.ToString() };

                        payed_u.ReadUser();
                        payed_u.VaultCoins += 1;
                        payed_u.ModifyUser();
                    })
                );

            // Build client
            _client = _builder.Build();

        }

        /// <summary>
        /// Start the discord client
        /// </summary>
        public async void Start()
        {
            await _client.ConnectAsync();
        }
    }
}
