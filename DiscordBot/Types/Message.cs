using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Message
    {


        public ulong DiscordID;
        public int Count;
        public int Warns;

        public Message(ulong DiscordID, int Count)
        {
            this.DiscordID = DiscordID;
            this.Count = Count;
        }

        public static bool Contains(ulong search)
        {
            return Cache.AntiSpam.Any(message => message.DiscordID == search);
        }

        public static Message Get(ulong search)
        {
            return Cache.AntiSpam.FirstOrDefault(message => message.DiscordID == search);
        }

        public static void SetMsgCount(ulong search)
        {
            Cache.AntiSpam.FirstOrDefault(message => message.DiscordID == search).Count += 1;
        }

        public static void SetWarnCount(ulong search)
        {
            Cache.AntiSpam.FirstOrDefault(message => message.DiscordID == search).Warns += 1;
        }
    }
}
