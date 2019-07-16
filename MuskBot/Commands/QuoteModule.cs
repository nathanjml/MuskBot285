using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MuskBot.Attributes;
using MuskBot.Handler;
using MuskBot.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class QuoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly ResponseAwaiter _responseAwaiter;
        private readonly IConfiguration _config;

        public QuoteModule(ResponseAwaiter response, IConfiguration config)
        {
            _responseAwaiter = response;
            _config = config;
        }

        [Command("musk")]
        public async Task TargetStudentAsync(string mentionedUser)
        {
            await ReplyAsync($"Please type the channel you want the bot to message the user {mentionedUser} in. ex: `chat` ");
            //WARNING: DO NOT AWAIT.
            //awaiting user input will block
            //prefer "fire and forget"
            new HandleMessageUser().HandleMessage(mentionedUser, _responseAwaiter, Context.User.Id, Context.Channel.Id, Context);
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

        public async Task HandleDictionaryLookup(SocketCommandContext ctx, string url)
        {
            using(var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
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

        public async Task HandleRPS(SocketCommandContext ctx, ulong mentionedUserId, ResponseAwaiter responseService)
        {
           var mentionedUser = await ctx.Channel.GetUserAsync(mentionedUserId);
           var result = await responseService.GetResponseFromUser(mentionedUserId, ctx.Channel.Id);
            if (!result.HasErrors)
            {
                if(result.Data.ToLower() == "y")
                {
                    await ctx.Channel.SendMessageAsync($"RPS! {ctx.User.Mention} vs. {mentionedUser.Mention}");
                    await ctx.Channel.SendMessageAsync("*Note: *MuskBot* will message you individually in a DM.*");
                    //start rps game.
                    var userOneDM = await ctx.User.GetOrCreateDMChannelAsync();
                    var userTwoDM = await mentionedUser.GetOrCreateDMChannelAsync();

                    await userOneDM.SendMessageAsync("type `rock` `paper` or `scissors` in this DM.");
                    var userOneResponse = await responseService.GetResponseFromUser(ctx.User.Id, userOneDM.Id);

                    await userTwoDM.SendMessageAsync("type `rock` `paper` or `scissors` in this DM.");
                    var userTwoResponse = await responseService.GetResponseFromUser(mentionedUserId, userTwoDM.Id);

                    if(!userOneResponse.HasErrors && !userTwoResponse.HasErrors)
                    {
                        await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} used {userOneResponse.Data} and {mentionedUser.Mention} used {userTwoResponse.Data}");
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync("A player didn't answer in time and the game has been canceled");
                        //timeout or error occured
                    }
                }
            }
        }
    }
}
