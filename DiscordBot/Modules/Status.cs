using CrispyCheats;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordBot.Modules
{
    public class Status : ModuleBase<SocketCommandContext>
    {
        Client client = Program.client;

        [Command("commands")]
        public async Task GetCommands()
        {
            Console.WriteLine("Commands Called");

            EmbedBuilder eb = new EmbedBuilder();
            eb.Description = "$wipe - Get Last Wipe (Aliases: w)" + "\n"
                           + "$status - Get Server Status (Aliases: s)" + "\n"
                           + "$cheats - Get Cheats List and Details (Aliases: C)" + "\n"
                           + "$membercount - Get Member Count abd Detauks (Aliases: mc)" + "\n"
                           + "$generatekey [Product] - Generates a Key for User (Aliases: gk)" + "\n"
                           + "$ban [Discord] or [Username] - Ban User from Discord and Client (Aliases: b)" + "\n"
                           + "$getaccount [Discord] or [Username] - Get Account Data (Aliases: ga)" + "\n"
                           + "$hwidreset [Discord] or [Username] - Resets Account HWID (Aliases: hr)" + "\n"
                           + "$passwordreset [Discord] or [Username] - Resets Account Password (Aliases: pr)" + "\n"
            ;
            eb.Color = Color.Blue;
            eb.Title = "MaverickBot Commands";

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        [Command("top")]
        [Alias("t")]
        public async Task GetServerResources()
        {
            Console.WriteLine("Top Called");

            await Context.Channel.SendMessageAsync("System CPU Usage: " + Functions.Counters.GetCPU().ToString("0.00") + "%" + "\n" 
                + "System RAM Usage: " + Functions.Counters.GetMem().ToString("0.00") + "GB" + "\n"
                + "Server CPU Usage: " + Functions.Counters.GetProcessCPU().ToString("0.00") + "%" + "\n"
                + "Server RAM Usage: " + Functions.Counters.GetProcessMem() + "MB" + "\n");
        }

        [Command("status")]
        [Alias("s")]
        public async Task GetStatus()
        {
            Console.WriteLine("Status Called");

            await Functions.Status.GetStatus(Context.Message, Context);
        }

        [Command("membercount")]
        [Alias("mc")]
        public async Task GetUserCount()
        {
            string token = "";

            Console.WriteLine("MemberCount Called Called");

            int fails = 0;
            TokenCheck:
            if (token == "")
            {
                client.Login(out token);
            }
            else
            {
                fails++;

                if (fails > 3)
                {
                    await Context.Message.Channel.SendMessageAsync("MemberCount Failure: We cannot valid the token for the bot at this time, sorry.");

                    return;
                }
                else
                    goto TokenCheck;
            }

            //Get Server Info ontop of Discord Info
            int DiscordMembers = Context.Guild.MemberCount;
            int OnlineMember = DiscordOnlineCount(Context.Guild);
            int LoginCount = 0;
            string OnlineCount = "";

            try
            {
                LoginCount = Convert.ToInt32(client.LoginCount(token));
                OnlineCount = ServerOnlineCount(client.OnlineCount(token));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.Description = 
                "Discord Members: " + DiscordMembers.ToString() + "\n" 
                + "Online Members: " + OnlineMember.ToString() + "\n" 
                + "Registered Users: " + LoginCount + "\n" 
                + OnlineCount;
            eb.Color = Color.Blue;
            eb.Title = "Member Count";

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        private int DiscordOnlineCount(SocketGuild guild)
        {
            int count = 0;

            foreach (SocketGuildUser User in guild.Users)
            {
                if (User.Status != UserStatus.Offline)
                {
                    count++;
                }
            }

            return count;
        }

        private string ServerOnlineCount(string ServerData)
        {
            string Response = "";

            if (!ServerData.Contains("|"))
                return "Players Online: 0";

            string[] datastruct = ServerData.Split('|');

            if (datastruct.Length > 1)
            {
                foreach (string data in datastruct)
                {
                    if (!data.Contains("-"))
                        continue;

                    string[] onlinestruct = data.Split('-');

                    if (onlinestruct.Length > 1)
                    {
                        string name = data.Split('-')[0];
                        int count = Convert.ToInt32(data.Split('-')[1]);

                        Response += name + " Players: " + count + '\n';
                    }
                }
            }

            return Response;
        }
    }
}
