using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class DictionaryModule : ModuleBase<SocketCommandContext>
    {
        private const string DictEndpoint = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";
        private readonly IConfiguration _config;

        public DictionaryModule(IConfiguration config)
        {
            _config = config;
        }

        [Command("define")]
        [Alias("def")]
        public async Task LookupWord(string word)
        {
            var finalEndpoint = $"{DictEndpoint}{word}?key={_config["dictionary"]}";
            new HandleMessageUser().HandleDictionaryLookup(Context, finalEndpoint);
        }
    }
}
