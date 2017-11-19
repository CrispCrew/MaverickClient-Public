using Discord;
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
    public class AccountInfo : ModuleBase<SocketCommandContext>
    {
        Client client = Program.client;

        [Command("getaccount")]
        [Alias("ga")]
        public async Task GetAccount(string Username)
        {
            //Check if its a username or mention
            bool Mention = !(Context.Message.MentionedUsers.Count == 0);

            //Store all mentioned users
            IReadOnlyCollection<SocketUser> Users = Context.Message.MentionedUsers;

            string Token;

            Console.WriteLine("Get Account Called");

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

            client.Login(out Token);

            Console.WriteLine("Login Token: " + Token);

            if (!Mention)
            {
                if (Username == "Username Not Found" || Username == "DiscordID not Specified" || Username == "Server Error" || Username == "")
                {
                    await Context.Message.Channel.SendMessageAsync("User does not have an account.");
                    return;
                }
                else
                {
                    await Context.Message.Channel.SendMessageAsync("Sent user data to DM's.");
                }

                var dm = await Context.Message.Author.GetOrCreateDMChannelAsync();

                string resp = client.AdminGetAccountDetails(Username, Token);

                if (resp != "Account not Found" && resp != "Invalid Permissions" && resp != "Token Not Specified" && resp != "Username Not Provided")
                {
                    string[] data = resp.Split('|');

                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Description =
                        "MemberID: " + data[0] + "\n"
                        + "Username: " + data[1] + "\n"
                        + "HWID: " + data[2] + "\n"
                        + "DiscordID: " + data[3] + "\n"
                        + "DiscordName: " + data[4] + "\n"
                        + "Is Banned: " + data[5] + "\n"
                        + "Is Admin: " + data[6] + "\n"
                        + "Owned Products: " + data[7] + "\n"
                        + "License Keys: " + data[8] + "\n";
                    eb.Color = Color.Blue;
                    eb.Title = "User Account Details";

                    await dm.SendMessageAsync("", false, eb);
                }
                else
                {
                    await dm.SendMessageAsync("Failed to Query Account: " + resp);
                }
            }
            else
            {
                foreach (SocketUser User in Users)
                {
                    Username = client.FindUser(User.Id);

                    if (Username == "User Not Found" || Username == "DiscordID not Specified" || Username == "Server Error" || Username == "")
                    {
                        await Context.Message.Channel.SendMessageAsync("User does not have an account.");

                        return;
                    }
                    else
                    {
                        await Context.Message.Channel.SendMessageAsync("Sent user data to DM's.");
                    }

                    var dm = await Context.Message.Author.GetOrCreateDMChannelAsync();

                    string resp = client.AdminGetAccountDetails(Username, Token);

                    if (resp != "Account not Found" && resp != "Invalid Permissions" && resp != "Token Not Specified" && resp != "Username Not Provided")
                    {
                        string[] data = resp.Split('|');

                        EmbedBuilder eb = new EmbedBuilder();
                        eb.Description =
                            "MemberID: " + data[0] + "\n"
                            + "Username: " + data[1] + "\n"
                            + "HWID: " + data[2] + "\n"
                            + "DiscordID: " + data[3] + "\n"
                            + "DiscordName: " + data[4] + "\n"
                            + "Is Banned: " + data[5] + "\n"
                            + "Is Admin: " + data[6] + "\n"
                            + "Owned Products: " + data[7] + "\n"
                            + "License Keys: " + data[8] + "\n";
                        eb.Color = Color.Blue;
                        eb.Title = "User Account Details";

                        await dm.SendMessageAsync("", false, eb);
                    }
                    else
                    {
                        await dm.SendMessageAsync("Failed to Query Account: " + resp);
                    }
                }
            }
        }
    }
}
