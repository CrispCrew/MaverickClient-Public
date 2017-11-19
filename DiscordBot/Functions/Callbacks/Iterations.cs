using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Functions
{
    public static class Iterations
    {
        #region Custom Iterations
        public static ITextChannel FindChannel(SocketGuild guild, string name)
        {
            return guild.TextChannels.FirstOrDefault(channel => channel.Name.ToLower() == name.ToLower()) as ITextChannel;
        }

        public static bool FindRole(SocketGuild guild, string name)
        {
            return guild.Roles.Any(role => role.Name.ToLower() == name.ToLower());
        }

        public static SocketRole GetRole(SocketGuild guild, string name)
        {
            return guild.Roles.FirstOrDefault(role => role.Name.ToLower() == name.ToLower());
        }

        public static bool HasRole(SocketGuildUser user, string name)
        {
            return user.Roles.Any(role => role.Name.ToLower() == name.ToLower());
        }
        #endregion
    }
}
