using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class MemeModule : ModuleBase<SocketCommandContext>
    {
        private readonly IConfiguration _config;

        public MemeModule(IConfiguration config)
        {
            _config = config;
        }

        [Command("meme")]
        [Alias("gif")]
        public async Task PostMemeAsync()
        {
            var apikey = _config["giphy"];
            var url = $"http://api.giphy.com/v1/gifs/random?api_key={apikey}&tag=elon musk";
            new HandleMessageUser().HandleMeme(Context, url);
        }
    }
}
