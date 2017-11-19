using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Types
{
    public class InviteLog
    {
        RestInviteMetadata InviteLink;
        SocketGuild Guild;

        #region Gets/Sets
        public RestInviteMetadata invitelink
        {
            get
            {
                return InviteLink;
            }
            set
            {
                InviteLink = value;
            }
        }

        public SocketGuild guild
        {
            get
            {
                return Guild;
            }
            set
            {
                Guild = value;
            }
        }
        #endregion

        public InviteLog()
        {

        }

        public InviteLog(RestInviteMetadata InviteLink, SocketGuild Guild)
        {
            this.InviteLink = InviteLink;
            this.Guild = Guild;
        }

        public static RestInviteMetadata FindInvite(List<InviteLog> temp, List<InviteLog> lastcache)
        {
            foreach(InviteLog invite in temp)
            {
                Console.WriteLine("Invite: " + invite.InviteLink.Code + " Guild: " + invite.Guild.Name);

                foreach (InviteLog cachedinvite in lastcache)
                {
                    Console.WriteLine("CachedInvite: " + cachedinvite.InviteLink.Code + " Guild: " + cachedinvite.Guild.Name);

                    if (invite.Guild.Name == cachedinvite.Guild.Name)
                    {
                        if (invite.InviteLink.Code == cachedinvite.InviteLink.Code)
                        {
                            if (invite.InviteLink.Uses > cachedinvite.InviteLink.Uses)
                            {
                                Console.WriteLine(invite.InviteLink.Uses + " > " + cachedinvite.InviteLink.Uses);

                                return invite.InviteLink;
                            }
                            else
                            {
                                Console.WriteLine(invite.InviteLink.Uses + " != " + cachedinvite.InviteLink.Uses);
                            }
                        }
                        else
                        {
                            Console.WriteLine(invite.InviteLink.Code + " != " + cachedinvite.InviteLink.Code);
                        }
                    }
                    else
                    {
                        Console.WriteLine(invite.Guild.Name + " != " + cachedinvite.Guild);
                    }
                }
            }

            return null;
        }
    }
}
