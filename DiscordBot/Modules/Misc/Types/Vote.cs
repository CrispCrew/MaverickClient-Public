using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Misc
{
    public class Vote
    {
        public ulong User;
        public int VotedFor;

        public Vote(ulong User, int VotedFor)
        {
            this.User = User;
            this.VotedFor = VotedFor;
        }
    }
}
