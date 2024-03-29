﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MuskBot.Core.CryptoService;
using MuskBot.Core.DictionaryService;
using MuskBot.Core.MemeService;
using MuskBot.Core.RpsGameService;
using MuskBot.Handler;
using MuskBot.Helper;
using MustBot.Handler;
using System.IO;
using System.Threading.Tasks;

namespace MuskBot
{
    class Program
    {

        private IConfiguration _config;
        private DiscordSocketClient _client;

        static void Main(string[] args)
    => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _config = Configure();
            var services = ConfigureServices();

            await _client.LoginAsync(TokenType.Bot, _config["token"]);
            await _client.StartAsync();

            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();


            await Task.Delay(-1);
        }

        private IServiceCollection ConfigureServices()
        {
            //register any dependencies here
            var services = new ServiceCollection()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<ResponseAwaiter>()
                .AddSingleton<CryptoCurrencyLookup>()
                .AddSingleton<IConfiguration>(_config)
                .AddSingleton<GiphyMemeService>()
                .AddSingleton<TenorMemeService>()
                .AddSingleton<MemeServiceFactory>()
                .AddSingleton<IDictionaryService, DictionaryService>()
                .AddSingleton<DiscordSocketClient>((x) => _client)
                .AddSingleton<BaseSocketClient>((x) => _client)
                .AddTransient<IRPSGameService, RPSGameService>()
                .AddTransient<ICryptoService, CryptoService>();

            return services;
        }

        private IConfiguration Configure()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            return config;
        }
    }
}
