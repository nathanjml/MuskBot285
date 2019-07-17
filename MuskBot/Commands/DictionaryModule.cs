using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MuskBot.Core.DictionaryService;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class DictionaryModule : ModuleBase<SocketCommandContext>
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryModule(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [Command("define")]
        [Alias("def")]
        public async Task LookupWord(string word)
        {
            _dictionaryService.LookupWord(Context, word);
        }
    }
}
