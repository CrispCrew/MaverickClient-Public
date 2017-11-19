using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using DiscordBot.API;
using DiscordBot.Functions;
using DiscordBot.Modules;
using DiscordBot.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Program
    {
        //public static List<SocketMessage> message = new List<SocketMessage>();

        public static Client client = new Client();

        public static bool active = false;

        public static string BotToken;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _handler;

        /// <summary>
        /// Do not open on stream
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, File.ReadAllLines("token.txt")[0]);

            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            Console.WriteLine("Starting Status Loop");

            Thread doStatus = new Thread(doChat);
            doStatus.IsBackground = true;
            doStatus.Start();

            await Task.Delay(-1);
        }

        public void doChat()
        {
            int ping = 9999;
            bool server;
            bool logins;
            int logincount;
            string online;

            //0 = Ping
            //1 = Discord Members
            //2 = Registered Members
            //3 = Online Members ( Cheat )
            while (true)
            {
                Client client = new Client();

                Console.WriteLine("Status Called");

                try
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    server = client.ServerCheck();

                    ping = (int)timer.ElapsedMilliseconds;

                    logins = client.Login(out BotToken);

                    Console.WriteLine("Token: " + BotToken);

                    //logincount = Convert.ToInt32(client.LoginCount(BotToken));
                    //online = client.OnlineCount(BotToken);

                    if (logins && server)
                    {
                        Console.WriteLine("Login: Online, Server: Online");

                        _client.SetGameAsync("Ping: " + ping);
                    }
                    else if (!logins && !server)
                    {
                        _client.SetGameAsync("Servers: Offline");
                    }
                    else
                    {
                        _client.SetGameAsync((server ? (logins ? "" : "Logins: Offline") : "Server: Offline"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    server = false;
                    logins = false;
                    ping = 9999;

                    _client.SetGameAsync((server ? (logins ? "" : "Logins: Offline") : "Server: Offline"));
                }

                Thread.Sleep(15000);
            }
        }
    }

    public class CommandHandler
    {
        public static List<InviteLog> invitelog = new List<InviteLog>();

        private DiscordSocketClient _client;

        private CommandService _service;

        public static bool SlowMode = false;
        public static int maxMessagesInterval = 5000;
        public static int maxMessagesPerInterval = 1;

        public static int requestCount = 0;
        public static int maxRequestsPerSecond = 5;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;

            //_client.UserJoined += HandleJoinAsync;

            Thread doRateLimiting = new Thread(CommandLimiter);
            doRateLimiting.IsBackground = true;
            doRateLimiting.Start();

            Thread doMessageLimiting = new Thread(MessageLimiter);
            doMessageLimiting.IsBackground = true;
            doMessageLimiting.Start();
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;

            if (msg == null)
                return;

            var Context = new SocketCommandContext(_client, msg);

            //Check Anti SPAM
            if (SlowMode && (!Iterations.HasRole((SocketGuildUser)Context.User, "Bots")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Mod")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Admin")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Junior Dev")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Supervisor")
                && !Iterations.HasRole((SocketGuildUser)Context.User, "Owner")
                && Message.Contains(Context.User.Id)))
            {
                Message message = Message.Get(Context.User.Id);

                Console.WriteLine("User: " + Context.User.Username + " Messages: {0}/{1} Warns: {2}/{3}", message.Count, maxMessagesPerInterval, message.Warns, 3);

                if (message.Count >= maxMessagesPerInterval)
                {
                    Message.SetWarnCount(Context.User.Id);

                    if (message.Warns > 3)
                    {
                        //Nothing yet
                    }

                    await Context.Message.DeleteAsync();
                }
                else
                {
                    Message.SetMsgCount(Context.User.Id);
                }
            }
            else
            {
                Cache.AntiSpam.Add(new Message(Context.User.Id, 1));
            }

            int argPos = 0;

            if (msg.HasCharPrefix('$', ref argPos))
            {
                if (msg.Content.ToLower().Contains("$gk"))
                {
                    await msg.DeleteAsync();

                    return;
                }

                requestCount++;

                Console.WriteLine("Request number " + requestCount + " Out of " + maxRequestsPerSecond);

                if (requestCount > maxRequestsPerSecond)
                {
                    return;
                }

                Console.WriteLine("Command: " + Context.Message);
                var result = await _service.ExecuteAsync(Context, argPos);

                if (!result.IsSuccess)// && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine("Command Error: " + result.Error.ToString());
                    await Context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && ((msg.Content.ToLower().Contains("download") && (msg.Content.ToLower().Contains("it") || msg.Content.ToLower().Contains("?"))) || msg.Content.ToLower().Contains(" cheat") || msg.Content.ToLower().Contains("loader") || msg.Content.ToLower().Contains("mod ") || msg.Content.ToLower().Contains("hack") || msg.Content.ToLower().Contains("menu") || msg.Content.ToLower().Contains("gk") || ((msg.Content.ToLower().Contains("gk") && msg.Content.ToLower().Contains("cs")) || (msg.Content.ToLower().Contains("gk") && msg.Content.ToLower().Contains("gta")))))
            {
                //Check Anti SPAM
                if (SlowMode && (!Iterations.HasRole((SocketGuildUser)Context.User, "Bots")
                    && !Iterations.HasRole((SocketGuildUser)Context.User, "Mod")
                    && !Iterations.HasRole((SocketGuildUser)Context.User, "Admin")
                    && !Iterations.HasRole((SocketGuildUser)Context.User, "Junior Dev")
                    && !Iterations.HasRole((SocketGuildUser)Context.User, "Supervisor")
                    && !Iterations.HasRole((SocketGuildUser)Context.User, "Owner")))
                {

                    await Context.User.SendMessageAsync("Discords Terms of Service Prohibit Sharing of 'Cheats', therefore you can find our Software on our Website." + "\n"
                     + "Software Download is on the forum: " + "\n"
                     + "https://MaverickCheats.eu/");

                    await Context.Message.DeleteAsync();
                }
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("activation") || msg.Content.ToLower().Contains("acitvate") || msg.Content.ToLower().Contains("license") || (msg.Content.ToLower().Contains("key") && msg.Content.ToLower().Contains("?")))
            {
                await Context.User.SendMessageAsync("License Keys are no longer required for the cheat, just login using your Forum Email and Password and you will have access to all the Free cheats.");

                await Context.Message.DeleteAsync();
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("hwid") && msg.Content.ToLower().Contains("taken"))
            {
                await Context.Channel.SendMessageAsync("HWID Taken means you have a Login already, please use your current account.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("hwid") && msg.Content.ToLower().Contains("invalid"))
            {
                await Context.Channel.SendMessageAsync("Invalid HWID means your Computer Identity has changed, ask staff for a reset.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("invalid") && msg.Content.ToLower().Contains("username"))
            {
                await Context.Channel.SendMessageAsync("Invalid Username means there is no account associated to that username, there may have been a database reset recently and you may need to register again.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && ((msg.Content.ToLower().Contains("stealth") && msg.Content.ToLower().Contains("broke")) || (msg.Content.ToLower().Contains("stealth") && msg.Content.ToLower().Contains("cant")) || (msg.Content.ToLower().Contains("stealth") && msg.Content.ToLower().Contains("wont")) || (msg.Content.ToLower().Contains("stealth") && msg.Content.ToLower().Contains("isnt"))))
            {
                await Context.Channel.SendMessageAsync("Stealth is buggy and may not work, if you encounter this issue you can try changing servers and restarting your game to help fix the issue.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("server down"))
            {
                await DiscordBot.Functions.Status.GetStatus(msg, Context);
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower() == "crash")
            {
                await Context.Channel.SendMessageAsync("If you have crashes when you immediately start your MaverickClient or Updater, you NEED to disable ALL POSSIBLE SECURITY that may block the connection. This cannot be fixed by us as AntiVirus's and Firewalls autoblock suspicous servers such as our 'Server'.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && (msg.Content.ToLower().Contains("vehicle") && msg.Content.ToLower().Contains("spawn") || msg.Content.ToLower().Contains("kicks") && msg.Content.ToLower().Contains("out") & msg.Content.ToLower().Contains("out")) && msg.Author.Id != _client.CurrentUser.Id)
            {
                Console.WriteLine(msg.Author.Id + "!=" + _client.CurrentUser.Id);

                await Context.Channel.SendMessageAsync("If you're having issues with vehicles, ensure the bypass is enabled by going to the Spawn Tab, Vehicles and then Options at the top.");
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("send") && msg.Content.ToLower().Contains("logs"))
            {
                Console.WriteLine("Logs Called");

                EmbedBuilder eb = new EmbedBuilder();
                eb.WithTitle("Finding our Logs");
                eb.AddInlineField("Loader Logs", "Opening MaverickClient shows a Black Console, right click the top of the console, click edit, click select all and press enter. You can now press CTRL + V in discord to paste the logs.");
                eb.AddInlineField("Game Logs", "Press Windows Key + R while on the Desktop.\nType in: %appdata%\\CrispyCheats\\ and press enter.");
                eb.Color = new Color(0, 255, 0);

                await msg.Channel.SendMessageAsync("", false, eb);
            }
            else if (msg.Author.Id != _client.CurrentUser.Id && msg.Content.ToLower().Contains("menu") && msg.Content.ToLower().Contains("open"))
            {
                Console.WriteLine("Menu Open Called");

                EmbedBuilder eb = new EmbedBuilder();
                eb.WithTitle("Menu Navigation");
                eb.AddInlineField("Open Menu", "Numpad -");
                eb.AddInlineField("Save Config", "L");
                eb.AddInlineField("Unload Menu", "F9");

                eb.AddInlineField("Tab Left", "Numpad 7");
                eb.AddInlineField("Menu Up", "Numpad 8");
                eb.AddInlineField("Tab Right", "Numpad 9");

                eb.AddInlineField("Menu Left", "Numpad 4");
                eb.AddInlineField("Toggle Option", "Numpad 5");
                eb.AddInlineField("Menu Right", "Numpad 6");
                eb.AddInlineField("Menu Back", "Numpad 0");
                eb.AddInlineField("Menu Down", "Numpad 2");
                eb.AddInlineField("Num Lock", "Toggles Controls");
                eb.Color = new Color(0, 255, 0);
                eb.Title = "Menu Keybinds";

                await msg.Channel.SendMessageAsync("", false, eb);
            }
        }

        private async Task HandleJoinAsync(SocketGuildUser s)
        {
            Console.WriteLine("New user joined: " + s.Guild);

            //await s.Guild.DefaultChannel.SendMessageAsync("Joined the Server!");

            foreach (SocketGuild guild in _client.Guilds)
            {
                Console.WriteLine("Guild: " + guild.Name);

                foreach (RestInviteMetadata invite in await guild.GetInvitesAsync())
                {
                    Console.WriteLine(invite.Code + " -> " + invite.Uses + " Guild: " + guild.Name);

                    CommandHandler.invitelog.Add(new InviteLog(invite, guild));
                }
            }

            List<InviteLog> templog = new List<InviteLog>();

            foreach (RestInviteMetadata invite in await s.Guild.GetInvitesAsync())
            {
                Console.WriteLine(invite.Code + " -> " + invite.Uses);

                templog.Add(new InviteLog(invite, s.Guild));
            }

            await Iterations.FindChannel(s.Guild, "bot-spam").SendMessageAsync(s.Mention + " joined using " + InviteLog.FindInvite(templog, invitelog).Code);

            templog = new List<InviteLog>();
            foreach (SocketGuild guild in _client.Guilds)
            {
                Console.WriteLine("Guild: " + guild.Name);

                foreach (RestInviteMetadata invite in await guild.GetInvitesAsync())
                {
                    Console.WriteLine(invite.Code + " -> " + invite.Uses + " Guild: " + guild.Name);

                    templog.Add(new InviteLog(invite, guild));
                }
            }

            invitelog = templog;
        }

        private void CommandLimiter()
        {
            while (true)
            {
                requestCount = 0;

                Console.WriteLine("Reset Rate Limiter");

                Thread.Sleep(1000);
            }
        }

        private void MessageLimiter()
        {
            int ms = 0;
            while (true)
            {
                if (!SlowMode)
                {
                    Thread.Sleep(1000);

                    continue;
                }

                //if SlowMode == true
                if (ms < maxMessagesInterval)
                {
                    Thread.Sleep(250);

                    ms += 250;

                    continue;
                }

                Cache.AntiSpam.Clear();

                Console.WriteLine("Reset Message Limiter");

                Thread.Sleep(250);

                ms = 0;
            }
        }
    }
}
