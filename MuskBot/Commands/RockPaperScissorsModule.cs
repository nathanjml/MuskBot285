using Discord.Commands;
using MuskBot.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class RockPaperScissorsModule : ModuleBase<SocketCommandContext>
    {
        private readonly ResponseAwaiter _responseAwaiter;

        public RockPaperScissorsModule(ResponseAwaiter responseAwaiter)
        {
            _responseAwaiter = responseAwaiter;
        }

        [Command("rps")]
        public async Task ChallengeRPS(string mentionedUser)
        {
            var id = Regex.Replace(mentionedUser, @"[^\d]", "");
            var ulongParsedid = ulong.Parse(id);
            await Context.Channel.SendMessageAsync($"Challenge created! {mentionedUser} must respond with `y` to start the game!");
            new HandleMessageUser().HandleRPS(Context, ulongParsedid, _responseAwaiter);
        }
    }
}
