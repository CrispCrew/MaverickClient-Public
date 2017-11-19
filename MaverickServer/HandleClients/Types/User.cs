using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients.Types
{
    public class User
    {
        int ID;
        string Username;
        string Password;

        public int id
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
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

        public string password
        {
            get
            {
                return Password;
            }
            set
            {
                Password = value;
            }
        }

        public User(int ID, string Username, string Password)
        {
            this.ID = ID;
            this.Username = Username;
            this.Password = Password;
        }
    }
}
