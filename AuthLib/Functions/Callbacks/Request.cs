using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CRISPY, I am unsure what these do
namespace AuthLib.Functions
{
    /// <summary>
    /// The AuthLib Requests Class
    /// </summary>
    public class Request
    {
        /// <summary>
        /// INSERT DESC
        /// <param name="Variable">INSERT DESC</param>
        /// <param name="Data">INSERT DESC</param>
        /// </summary>
        public static bool Contains(string Variable, string Data)
        {
            if (!Data.Contains(Variable + "="))
                return false;

            return true;
        }

        /// <summary>
        /// INSERT DESC
        /// <param name="Variable">INSERT DESC</param>
        /// <param name="Data">INSERT DESC</param>
        /// </summary>
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
