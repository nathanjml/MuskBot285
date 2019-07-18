using Discord.Commands;
using MuskBot.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MuskBot.Core.RpsGameService
{
    public interface IRPSGameService
    {
        Task CreateGame(SocketCommandContext context, ulong mentionedUserId);
    }

    public class RPSGameService : IRPSGameService
    {
        private readonly ResponseAwaiter _responseAwaiter;

        public RPSGameService(ResponseAwaiter responseAwaiter)
        {
            _responseAwaiter = responseAwaiter;
        }

        public async Task CreateGame(SocketCommandContext ctx, ulong mentionedUserId)
        {
            var mentionedUser = await ctx.Channel.GetUserAsync(mentionedUserId);
            var result = await _responseAwaiter.GetResponseFromUser(mentionedUserId, ctx.Channel.Id);
            if (!result.HasErrors)
            {
                if (result.Data.ToLower() == "y")
                {
                    await ctx.Channel.SendMessageAsync($"RPS! {ctx.User.Mention} vs. {mentionedUser.Mention}");
                    await ctx.Channel.SendMessageAsync("*Note: *MuskBot* will message you individually in a DM.*");
                    //start rps game.
                    var userOneDM = await ctx.User.GetOrCreateDMChannelAsync();
                    var userTwoDM = await mentionedUser.GetOrCreateDMChannelAsync();

                    await userOneDM.SendMessageAsync("type `rock` `paper` or `scissors` in this DM.");
                    var userOneResponse = await _responseAwaiter.GetResponseFromUser(ctx.User.Id, userOneDM.Id);

                    await userTwoDM.SendMessageAsync("type `rock` `paper` or `scissors` in this DM.");
                    var userTwoResponse = await _responseAwaiter.GetResponseFromUser(mentionedUserId, userTwoDM.Id);

                    if (!userOneResponse.HasErrors && !userTwoResponse.HasErrors)
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
