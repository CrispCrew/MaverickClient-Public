using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.Types
{
    public class Request
    {
        public string IP;
        public int Attempts;
        public DateTime Time;

        public Request()
        {

        }

        public Request(string IP, int Attempts, DateTime Time)
        {
            this.IP = IP;
            this.Attempts = Attempts;
            this.Time = Time;
        }
    }
}
