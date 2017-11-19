using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Misc
{
    public class Polls : ModuleBase<SocketCommandContext>
    {
        public static List<Poll> polls = new List<Poll>();

        [Command("create poll")]
        public async Task CreatePoll(params string[] Parameters)
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

            if (!Parameters[0].StartsWith("<"))
            {
                await Context.Channel.SendMessageAsync("You must surround the parameters of this command in <> to ensure the bot can insert the full Poll Name and Poll Options!");
            }

            List<string> arguments = new List<string>();

            StringBuilder temp = new StringBuilder();
            foreach (string arg in Parameters.ToList())
            {
                if (arg.EndsWith(">"))
                {
                    temp.Append(arg);

                    arguments.Add(temp.ToString().Replace("<", "").Replace(">", ""));

                    temp.Clear();
                }
                else
                {
                    temp.Append(arg + " ");
                }
            }

            string PollName = arguments[0];
            string PollOptionString = "";

            int index = 1;
            foreach (string PollOption in arguments.GetRange(1, (arguments.Count - 1)))
                PollOptionString += (index++) + ": " + PollOption + "\n";

            polls.Add(new Poll((polls.Count + 1), PollName, arguments.GetRange(1, (arguments.Count - 1)).ToArray()));

            await Context.Channel.SendMessageAsync("Poll Created: " + "ID: " + polls.Count + " Name: " + PollName + "\n" + PollOptionString);
        }

        [Command("start poll")]
        public async Task StartPoll()
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

            Console.WriteLine("Starting Poll");

            Poll LastPoll = polls.Last();

            Console.WriteLine(LastPoll.PollName);

            string MessageBuilder = "";

            int index = 1;
            foreach (string options in LastPoll.PollOptions)
            {
                MessageBuilder += index + ": " + "[" + options + "]" + "\n";

                index++;
            }

            Console.WriteLine(MessageBuilder);

            await Iterations.FindChannel(Context.Guild, "news").SendMessageAsync(Context.Guild.EveryoneRole.Mention + "\nPoll " + LastPoll.PollName + " has been started by " + Context.User.Mention + ".\n"
                + "To vote now, type $vote with the number thats next to the option you're voting for like this `$vote 1`" + "\n\n"
                + MessageBuilder
                );


            Console.WriteLine("Sent");
        }

        [Command("vote")]
        public async Task VoteOnPoll(int PollVote)
        {
            Poll activePoll = polls.Last();

            if (PollVote != 0 && activePoll.PollOptions.Length < PollVote)
            {
                await Context.Channel.SendMessageAsync(activePoll.PollName + " does not have this option!");

                return;
            }

            if (!activePoll.Votes.Any(vote => vote.User == Context.User.Id))
            {
                activePoll.Votes.Add(new Vote(Context.User.Id, PollVote));

                await Context.Channel.SendMessageAsync(Context.User.Mention + " has voted!");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.User.Mention + " you have already voted!");
            }
        }

        [Command("vote")]
        public async Task VoteOnPoll(int PollID, int PollVote)
        {
            Poll activePoll = polls.FirstOrDefault(Poll => Poll.PollID == PollID);

            if (PollVote != 0 && activePoll.PollOptions.Length < PollVote)
            {
                await Context.Channel.SendMessageAsync(activePoll.PollName + " does not have this option!");

                return;
            }

            if (!activePoll.Votes.Any(vote => vote.User == Context.User.Id))
            {
                activePoll.Votes.Add(new Vote(Context.User.Id, PollVote));

                await Context.Channel.SendMessageAsync(Context.User.Mention + " has voted!");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Context.User.Mention + " you have already voted!");
            }
        }

        [Command("results")]
        public async Task CheckResults()
        {
            Poll activePoll = polls.Last();

            string MessageBuilder = "";

            int index = 1;
            foreach (string options in activePoll.PollOptions)
            {
                MessageBuilder += index + ": " + "[" + options + "] " + activePoll.Votes.Where(vote => vote.VotedFor == index).Count() + "\n";

                index++;
            }

            await Context.Channel.SendMessageAsync(activePoll.PollName + " has " + activePoll.Votes.Count + "\n" + MessageBuilder);
        }

        [Command("results")]
        public async Task CheckResults(int PollID)
        {
            Poll activePoll = polls.FirstOrDefault(Poll => Poll.PollID == PollID);

            string MessageBuilder = "";

            int index = 1;
            foreach (string options in activePoll.PollOptions)
            {
                MessageBuilder += index + ": " + "[" + options + "] " + activePoll.Votes.Where(vote => vote.VotedFor == index).Count() + "\n";

                index++;
            }

            await Context.Channel.SendMessageAsync(activePoll.PollName + " has " + activePoll.Votes.Count + "\n" + MessageBuilder);
        }

        [Command("end Poll")]
        public async Task EndPoll()
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

            polls.RemoveAt(polls.Count() - 1);

            await Context.Channel.SendMessageAsync("Active pole has ended!");
        }

        [Command("end Poll")]
        public async Task EndPoll(int PollID)
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

            Poll activePoll = polls.FirstOrDefault(Poll => Poll.PollID == PollID);

            await Context.Channel.SendMessageAsync(activePoll.PollName + " has ended!");
        }
    }
}
