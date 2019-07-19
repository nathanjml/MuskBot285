using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MuskBot.Core.MemeService
{
    public interface IMemeService
    {
        Task GetMeme(SocketCommandContext scc, string tags);
    }

    public class GiphyMemeService : IMemeService
    {
        private readonly string url;
        public GiphyMemeService(IConfiguration config)
        {
            url = $"http://api.giphy.com/v1/gifs/random?api_key={config["giphy"]}"; 
        }

        public async Task GetMeme(SocketCommandContext scc, string tags)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{url}&tag={tags}");
                var responseContent = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    //extract just the gif url to post to the channel
                    var obj = JObject.Parse(responseContent).SelectToken("data").Value<string>("url");
                    await scc.Channel.SendMessageAsync(obj);
                }
            }
        }
    }

    public class TenorMemeService : IMemeService
    {
        private readonly string url;

        public TenorMemeService(IConfiguration config)
        {
            url = $"https://api.tenor.com/v1/random?key={config["tenor"]}";
        }

        public async Task GetMeme(SocketCommandContext scc, string tags)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{url}&q={tags}&limit=1");
                var responseContent = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    //extract just the gif url to post to the channel
                    var obj = JObject.Parse(responseContent).Value<JArray>("results");
                    var url = obj[0].Value<string>("url");
                    await scc.Channel.SendMessageAsync(url);
                }
            }
        }
    }
}
