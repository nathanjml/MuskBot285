using Discord;
using Discord.Commands;
using MuskBot.Core.CryptoService;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class CryptoModule : ModuleBase<SocketCommandContext>
    {
        private readonly ICryptoService _cryptoService;

        public CryptoModule(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [Command("crypto")]
        public async Task GetCryptoCurrencyPrice(string identifier = "BTC")
        {
            var result = await _cryptoService.GetCurrencyInfo(identifier);
            if (result.WasSuccessful)
            {
                var embed = new EmbedBuilder();

                embed.WithTitle($"{identifier} - USD");
                embed.WithAuthor(Context.Message.Author);
                embed.WithColor(Color.Purple);
                embed.AddField("Fetched on", result.Data.ValuedAt.ToString(), true);
                embed.AddField("Name", result.Data.Name, true);
                embed.AddField("Value", result.Data.Value, true);
                var e = embed.Build();

                await ReplyAsync(embed: e);
            }
            else
            {
                await ReplyAsync($"{identifier} could not be found or is not supported at this time.");
            }
        }
    }
}
