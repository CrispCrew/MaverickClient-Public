using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLib.Functions.Callbacks
{
    /// <summary>
    /// The AuthLib Callback String Preparer
    /// </summary>
    class Prepare
    {
        /// <summary>
        /// Prepare the string
        /// <param name="input">The string to prepare</param>
        /// </summary>
        public static bool PrepareString(string input)
        {
            //Is the string invalid?
            if (input == null || input == "" || input == " ")
                return false;

            return true;
        }
    }
}
