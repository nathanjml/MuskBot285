using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MuskBot.Core.MemeService;
using System.Linq;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class MemeModule : ModuleBase<SocketCommandContext>
    {
        private readonly MemeServiceFactory _memeServiceFactory;

        public MemeModule(MemeServiceFactory memeService)
        {
            _memeServiceFactory = memeService;
        }

        [Command("meme")]
        [Alias("gif")]
        public async Task PostMemeAsync(string tags = "elon-musk", string flag = "-t")
        {
            var service = _memeServiceFactory.Create(flag);
            service.GetMeme(Context, tags);
        }

        [Command("lmgtfy")]
        public async Task GetGoogleLink(params string[] words)
        {
            var combined = words.Aggregate((x, y) => x + "+" + y);
            var url = "https://lmgtfy.com/?q=" + combined;
            await ReplyAsync(url);
        }

        [Command("google")]
        public async Task GetDirectGoogleLink(params string[] words)
        {
            var combined = words.Aggregate((x, y) => x + "+" + y);
            var url = "https://www.google.com/search?q=" + combined;
            await ReplyAsync(url);
        }
    }
}
