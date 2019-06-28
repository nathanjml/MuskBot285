using Discord.Commands;
using MuskBot.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("online")]
        [Alias("ping")]
        public Task ReplyOnlineAsnyc()
            => ReplyAsync("The **MUSK** is awake.");
    }
}
