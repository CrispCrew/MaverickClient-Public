using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients.Tokens
{
    public class Token
    {
        public string IP;
        public int ID;
        public string Username;
        public string AuthToken;
        public string LastDevice;
        public DateTime LastRequest;

        #region Gets / Sets
        public string ip
        {
            get
            {
                return IP;
            }
            set
            {
                IP = value;
            }
        }

        public string username
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
            }
        }

        public string authtoken
        {
            get
            {
                return AuthToken;
            }
            set
            {
                AuthToken = value;
            }
        }

        public string lastdevice
        {
            get
            {
                return LastDevice;
            }
            set
            {
                LastDevice = value;
            }
        }

        public DateTime lastrequest
        {
            get
            {
                return LastRequest;
            }
            set
            {
                LastRequest = value;
            }
        }
        #endregion

        public Token(string IP, int ID, string Username, string AuthToken)
        {
            this.IP = IP;
            this.ID = ID;
            this.Username = Username;
            this.AuthToken = AuthToken;
            this.LastRequest = DateTime.Now;
        }

        public Token(string IP, int ID, string Username, string LastDevice, string AuthToken)
        {
            this.IP = IP;
            this.ID = ID;
            this.Username = Username;
            this.AuthToken = AuthToken;
            this.LastDevice = LastDevice;
            this.LastRequest = DateTime.Now;
        }
    }
}