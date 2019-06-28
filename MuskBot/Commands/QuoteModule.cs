using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MuskBot.Attributes;
using MuskBot.Handler;
using MuskBot.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class QuoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly ResponseAwaiter _messageInt;
        private readonly IConfiguration _config;

        public QuoteModule(ResponseAwaiter response, IConfiguration config)
        {
            _messageInt = response;
            _config = config;
        }

        [Command("musk")]
        public async Task TargetStudentAsync(string mentionedUser)
        {
            await ReplyAsync($"Please type the channel you want the bot to message the user {mentionedUser} in. ex: `chat` ");
            //WARNING: DO NOT AWAIT.
            //awaiting user input will block
            //prefer "fire and forget"
            new HandleMessageUser().HandleMessage(mentionedUser, _messageInt, Context.User.Id, Context.Channel.Id, Context);
        }

        [Command("quote")]
        public async Task QouteAsync()
        {
            await ReplyAsync(Quips.GetRandomQuip());
        }
    }
    
    //TODO: Pull out to a mediator
    public class HandleMessageUser
    {
        //TODO: reduce this function
        public async Task HandleMessage(string mention, ResponseAwaiter response, ulong callerId, ulong callingChannelId, SocketCommandContext ctx)
        {
            var id = Regex.Replace(mention, @"[^\d]", "");
            var result = await response.GetResponseFromUser(callerId, callingChannelId);
            if (!result.HasErrors)
            {
                var allUsers = ctx.Guild.Users.ToList();
                var channels = ctx.Guild.Channels.ToList();

                var channelMention = result.Data;
                var channel = channels.FirstOrDefault(x => x.Name == channelMention);

                if (channel != null)
                {
                    var userFound = allUsers.FirstOrDefault(x => x.Id.ToString() == id);

                    if (userFound != null)
                    {
                        if (channel.Users.Contains(userFound))
                        {
                            var textChannel = (ITextChannel)channel;
                            await textChannel.SendMessageAsync(Quips.GetRandomQuip(userFound.Mention));
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync($"{channel.Name} does not contain that user...");
                        }
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync($"**The Musk** could not locate that user - {mention}.");
                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Do not try to fool the Musk. That channel is nonexistent.");
                }
            } //else time-out occured
        }
        public async Task HandleMeme(SocketCommandContext ctx, string url)
        {
            using(var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                var responseContent = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    //extract just the gif url to post to the channel
                    var obj = JObject.Parse(responseContent).SelectToken("data").Value<string>("url");
                    await ctx.Channel.SendMessageAsync(obj);
                }
            }
        }
    }
}
