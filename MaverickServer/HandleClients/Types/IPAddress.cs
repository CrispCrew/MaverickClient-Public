using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients.Types
{
    public class IP_Address
    {
        public static List<IP_Address> CachedIPs = new List<IP_Address>();

        string IP;
        bool Ban;

        public string ip
        {
            get
            {
                return IP;
            }
        }

        public bool ban
        {
            get
            {
                return Ban;
            }
        }

        public IP_Address()
        {

        }

        public IP_Address(string IP, bool Ban)
        {
            this.IP = IP;
            this.Ban = Ban;
        }

        public static bool HasIP(string IP)
        {
            foreach (IP_Address ipaddress in CachedIPs)
            {
                if (ipaddress.ip == IP)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsBanned(string IP)
        {
            foreach (IP_Address ipaddress in CachedIPs)
            {
                if (ipaddress.ip == IP)
                {
                    if (ipaddress.ban)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
