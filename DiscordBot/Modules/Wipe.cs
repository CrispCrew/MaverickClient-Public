using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Wipe : ModuleBase<SocketCommandContext>
    {
        [Command("wipe")]
        [Alias("w")]
        public async Task LastWipe()
        {
            await Context.Channel.SendMessageAsync("Last Wipe: [2017-10-13]");
        }
    }
}
