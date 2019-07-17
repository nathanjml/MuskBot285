using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MuskBot.Core.DictionaryService
{
    public interface IDictionaryService
    {
        Task LookupWord(SocketCommandContext ctx, string word);
    }

    public class DictionaryService : IDictionaryService
    {
        private readonly string apiKey;
        private const string DictEndpoint = "https://www.dictionaryapi.com/api/v3/references/collegiate/json/";

        public DictionaryService(IConfiguration config)
        {
            apiKey = config["dictionary"];
        }

        public async Task LookupWord(SocketCommandContext ctx, string word)
        {
            using (var client = new HttpClient())
            {
                var finalEndpoint = $"{DictEndpoint}{word}?key={apiKey}";

                var result = await client.GetAsync(finalEndpoint);
                if (result.IsSuccessStatusCode)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    try
                    {
                        var rawJson = JArray.Parse(responseContent);
                        var firstLevel = rawJson[0];
                        var fl = firstLevel["fl"].Value<string>();
                        var defs = firstLevel["shortdef"].Values<string>();

                        var embed = new EmbedBuilder();

                        embed.WithTitle(fl);
                        embed.WithAuthor(ctx.Message.Author);
                        embed.WithColor(Color.Green);
                        AddFields(defs, "definition:", embed);
                        var e = embed.Build();

                        await ctx.Channel.SendMessageAsync(embed: e);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        private EmbedBuilder AddFields(IEnumerable<string> strings, string fieldName, EmbedBuilder builder)
        {
            foreach (var s in strings)
            {
                builder.AddField(fieldName, s, true);
            }
            return builder;
        }
    }
}
