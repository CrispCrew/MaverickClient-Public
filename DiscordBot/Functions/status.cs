using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBot.Functions
{
    public class CustomWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 1000;
            return w;
        }
    }

    public class Status
    {
        public static Client client = Program.client;
        public static CustomWebClient webClient = new CustomWebClient();

        public static async Task GetStatus(SocketUserMessage message, SocketCommandContext Context)
        {
            Console.WriteLine("Status Called");

            bool server;
            bool logins;
            Color color;
            int ping;
            float CPU = Counters.GetCPU();
            double Memory = Counters.GetMem();
            string token;

            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                server = client.ServerCheck();

                ping = (int)timer.ElapsedMilliseconds;

                logins = client.Login(out token);

                if (logins && server)
                {
                    color = GetColor(Math.Min(ping, 150));
                }
                else if (!logins && !server)
                {
                    ping = 9999;
                    color = Color.Red;
                }
                else
                {
                    color = Color.Gold;
                }

                int members =  Context.Guild.MemberCount;

                EmbedBuilder eb = new EmbedBuilder();
                eb.Description = "Central Server: " + (server ? "Online" : "Offline") + "\n" +
                    "Login System: " + (logins ? "Online" : "Offline") + "\n" +
                    "CPU: " + CPU.ToString("0.00") + "%/100%\n" +
                    "RAM: " + (int)Memory + "GB/32GB\n" +
                    "Ping: " + ping;
                eb.Color = color;

                eb.Title = "Server Status";

                await message.Channel.SendMessageAsync("", false, eb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await message.Channel.SendMessageAsync("Reconnecting to the Server....");
                await message.Channel.SendMessageAsync("Try the command again!");
            }
        }

        private static Color GetColor(Int32 actualValue)
        {
            Int32 rangeStart = 0;
            Int32 rangeEnd = 150;

            Int32 max = rangeEnd - rangeStart; // make the scale start from 0
            Int32 value = actualValue - rangeStart; // adjust the value accordingly

            Int32 red = (255 * value) / max; // calculate green (the closer the value is to max, the greener it gets)
            Int32 green = 255 - red; // set red as inverse of green

            return new Color(red, green, 0);
        }
    }
}
