using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.Misc
{
    public class Poll
    {
        public int PollID;
        public string PollName;
        public string[] PollOptions;
        public List<Vote> Votes = new List<Vote>();

        public Poll(int PollID, string PollName, string[] PollOptions)
        {
            this.PollID = PollID;
            this.PollName = PollName;
            this.PollOptions = PollOptions;
        }
    }
}
