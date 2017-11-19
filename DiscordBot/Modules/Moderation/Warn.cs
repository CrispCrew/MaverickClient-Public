using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Moderation
{
    public class Warn : ModuleBase<SocketCommandContext>
    {
        [Command("warn")]
        public async Task WarnUser(SocketUser user)
        {
            if (Context.Message.Author.Id != 193255051807031296 &&
                (!Iterations.HasRole((SocketGuildUser)Context.User, "Mod")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Admin")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Junior Dev")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Supervisor")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Owner")))
            {
                await Context.Message.Channel.SendMessageAsync("Permission Denied.");
                return;
            }

            await user.SendMessageAsync("Discords Terms of Service Prohibit Sharing of 'Cheats' and any discussion related to them, therefore you can find our Software and support for our Software on our Website." + "\n"
                    + "Software Download is on the forum: " + "\n"
                    + "https://MaverickCheats.eu/");

            await Context.Channel.SendMessageAsync("User Warned!");
        }
    }
}
