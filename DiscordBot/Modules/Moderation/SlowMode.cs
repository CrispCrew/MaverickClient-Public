using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class SlowMode : ModuleBase<SocketCommandContext>
    {
        [Command("slowmode")]
        [Alias("sm")]
        public async Task SlowModeToggle()
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

            await Context.Channel.SendMessageAsync("Slow Mode: " + (CommandHandler.SlowMode = !CommandHandler.SlowMode).ToString() + " MessagesPerInterval: " + CommandHandler.maxMessagesPerInterval + " MessageInterval: " + CommandHandler.maxMessagesInterval + "ms");
        }

        [Command("slowmode")]
        [Alias("sm")]
        public async Task SlowModeToggle(int Messages, int Seconds)
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

            await Context.Channel.SendMessageAsync("Slow Mode: " + (CommandHandler.SlowMode = true) + " MessagesPerInterval: " + (CommandHandler.maxMessagesPerInterval = Messages) + " MessageInterval: " + (CommandHandler.maxMessagesInterval = (Seconds * 1000)) + "ms");
        }
    }
}
