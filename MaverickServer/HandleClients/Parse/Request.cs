using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Functions
{
    public class Request
    {
        public static bool Contains(string Variable, string Data)
        {
            if (!Data.Contains(Variable + "="))
                return false;

            return true;
        }

        public static string Get(string Variable, string Data)
        {
            if (!Data.Contains(Variable + "="))
                return null;

            string[] posted = Data.Split('&');

            foreach (string post in posted)
            {
                if (post.Contains(Variable + "="))
                {
                    return post.Replace(Variable + "=", "");
                }
            }

            return Data;
        }
    } 
}
