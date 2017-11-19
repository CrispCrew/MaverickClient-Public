using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.API;
using DiscordBot.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class AccountHandler : ModuleBase<SocketCommandContext>
    {
        Client client = Program.client;

        [Command("hwidreset")]
        [Alias("hr")]
        public async Task HWIDReset(string Username)
        {
            //If we're using a Username or Discord Mention
            bool Mention = !(Context.Message.MentionedUsers.Count == 0);

            //Mentioned List
            IReadOnlyCollection<SocketUser> Users = Context.Message.MentionedUsers;

            string Token;

            Console.WriteLine("hwidreset Called");

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

            //get a Admin token for administering admin actions
            client.Login(out Token);

            Console.WriteLine("Login Token: " + Token);

            if (!Mention)
            {
                if (client.ResetHWID(Username, Token))
                {
                    Console.WriteLine("HWID Reset");
                    await Context.Message.Channel.SendMessageAsync("HWID Reset: " + Username);
                }
                else
                {
                    Console.WriteLine("Failed to Reset HWID");
                    await Context.Message.Channel.SendMessageAsync("Failed to Reset HWID: " + Username);
                }
            }
            else
            {
                foreach (SocketUser User in Users)
                {
                    //Send API Request to ban all associated cheat accounts
                    Username = client.FindUser(User.Id);

                    if (Username != "User Not Found" && client.ResetHWID(Username, Token))
                    {
                        Console.WriteLine("HWID Reset");
                        await Context.Message.Channel.SendMessageAsync("HWID Reset: " + Username);
                    }
                    else
                    {
                        Console.WriteLine("Failed to Reset HWID");
                        await Context.Message.Channel.SendMessageAsync("Failed to Reset HWID: " + Username);
                    }
                }
            }
        }

        [Command("passwordreset")]
        [Alias("pr")]
        public async Task PasswordReset(string Username)
        {
            //If we're using a Username or Discord Mention
            bool Mention = !(Context.Message.MentionedUsers.Count == 0);

            //Mentioned List
            IReadOnlyCollection<SocketUser> Users = Context.Message.MentionedUsers;

            string Token;

            Console.WriteLine("Password Reset Called");

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

            //get a Admin token for administering admin actions
            client.Login(out Token);

            Console.WriteLine("Login Token: " + Token);

            if (!Mention)
            {
                string Response = client.ResetPassword(Username, Token);

                Console.WriteLine("Password Reset: " + Response);

                if (Response == "Password Reset")
                {
                    await Context.Message.Channel.SendMessageAsync("Password Reset: " + Username);
                }
                else
                {
                    await Context.Message.Channel.SendMessageAsync("Password Failed to be Reset: " + Response);
                }
            }
            else
            {
                foreach (SocketUser User in Users)
                {
                    //Send API Request to ban all associated cheat accounts
                    Username = client.FindUser(User.Id);

                    if (Username != "User Not Found")
                    {
                        string Response = client.ResetPassword(Username, Token);
                        
                        if (Response == "Password Reset")
                        {
                            await Context.Message.Channel.SendMessageAsync("Password Reset: " + Username);
                        }
                        else
                        {
                            await Context.Message.Channel.SendMessageAsync("Password Failed to Reset: " + Response);
                        }
                    }
                    else
                    {
                        await Context.Message.Channel.SendMessageAsync("Password Failed to be Reset: User not Found");
                    }
                }
            }
        }

        [Command("ban")]
        [Alias("b")]
        public async Task BanUser(string Username)
        {
            //If we're using a Username or Discord Mention
            bool Mention = !(Context.Message.MentionedUsers.Count == 0);

            //Mentioned List
            IReadOnlyCollection<SocketUser> Users = Context.Message.MentionedUsers;

            string Token;

            Console.WriteLine("Ban Called");

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

            //get a Admin token for administering admin actions
            client.Login(out Token);

            Console.WriteLine("Login Token: " + Token);

            if (!Mention)
            {
                if (client.Ban(Username, Token))
                {
                    Console.WriteLine("User Banned");
                    await Context.Message.Channel.SendMessageAsync("Banned " + Username + " From Client");
                }
                else
                {
                    Console.WriteLine("User Not Banned");
                    await Context.Message.Channel.SendMessageAsync("Failed to Ban " + Username);
                }
            }
            else
            {
                foreach (SocketUser User in Users)
                {
                    //Generate instance of a DM Channel
                    var dm = await User.GetOrCreateDMChannelAsync();

                    //Send API Request to ban all associated cheat accounts
                    Username = client.FindUser(User.Id);

                    if (Username != "User Not Found" && client.Ban(Username, Token))
                    {
                        await Context.Message.Channel.SendMessageAsync("Banned " + Username + " From Client");
                        await dm.SendMessageAsync("Your Login has been banned from the MaverickCheats Cheat Client. Have a good day!");
                    }
                    else
                    {
                        await Context.Message.Channel.SendMessageAsync("Failed to Ban " + Username);
                    }

                    //Tell the Person
                    await dm.SendMessageAsync("You have been banned from MaverickCheats Discord. Have a good day!");

                    //Tell the Server
                    await Context.Message.Channel.SendMessageAsync("Banned " + "<@" + User.Id + ">" + " From Discord");

                    //Finally Ban User
                    await Context.Guild.AddBanAsync(User);
                }
            }
        }

        [Command("ban")]
        [Alias("b")]
        public async Task BanUser(string Username, params string[] ReasonParams)
        {
            string Reason = "";

            foreach (string reason in ReasonParams)
                Reason += reason + " ";

            //If we're using a Username or Discord Mention
            bool Mention = !(Context.Message.MentionedUsers.Count == 0);

            //Mentioned List
            IReadOnlyCollection<SocketUser> Users = Context.Message.MentionedUsers;

            string Token;

            Console.WriteLine("Ban Called");

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

            //get a Admin token for administering admin actions
            client.Login(out Token);

            Console.WriteLine("Login Token: " + Token);

            if (!Mention)
            {
                if (client.Ban(Username, Token))
                {
                    Console.WriteLine("User Banned");
                    await Context.Message.Channel.SendMessageAsync("Banned " + Username + " From Client");
                }
                else
                {
                    Console.WriteLine("User Not Banned");
                    await Context.Message.Channel.SendMessageAsync("Failed to Ban " + Username);
                }
            }
            else
            {
                foreach (SocketUser User in Users)
                {
                    //Generate instance of a DM Channel
                    var dm = await User.GetOrCreateDMChannelAsync();

                    //Send API Request to ban all associated cheat accounts
                    Username = client.FindUser(User.Id);

                    if (Username != "User Not Found" && client.Ban(Username, Token))
                    {
                        await Context.Message.Channel.SendMessageAsync("Banned " + Username + " From Client");
                        await dm.SendMessageAsync("Your Login has been banned from the MaverickCheats Cheat Client for '" + Reason + "'. Have a good day!");
                    }
                    else
                    {
                        await Context.Message.Channel.SendMessageAsync("Failed to Ban " + Username);
                    }

                    //Tell the Person
                    await dm.SendMessageAsync("You have been banned from MaverickCheats Discord for '" + Reason + "'. Have a good day!");

                    //Tell the Server
                    await Context.Message.Channel.SendMessageAsync("Banned " + "<@" + User.Id + ">" + " From Discord");

                    //Finally Ban User
                    await Context.Guild.AddBanAsync(User);
                }
            }
        }
    }
}
