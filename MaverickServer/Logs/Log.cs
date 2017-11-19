using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.Logs
{
    public class Log
    {
        private static readonly object lockset = new object();

        //Class Constructor
        public Log()
        {

        }

		public static void WriteLine(string input, string logtype)
        {
            lock (lockset)
            {
                using (StreamWriter stream = File.AppendText(Environment.CurrentDirectory + "\\Logs\\" + logtype + ".txt"))
                {
                    stream.WriteLineAsync("[" + DateTime.Now.ToString() + "] " + input);
                }
            }
        }
    }
}
