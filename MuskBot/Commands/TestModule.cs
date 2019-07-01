using Discord.Commands;
using System.IO;
using System.Threading.Tasks;

namespace MuskBot.Commands
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("online")]
        [Alias("ping")]
        public Task ReplyOnlineAsnyc()
            => ReplyAsync("The **MUSK** is awake.");

        public async Task SetAvatar()
        {
            var avatar = new FileStream(Directory.GetCurrentDirectory() + "/Assets/deepfried_1561993344865.png", FileMode.Open);
            await Context.Client.CurrentUser.ModifyAsync(x => x.Avatar = new Discord.Image(avatar));
        }
    }
}
