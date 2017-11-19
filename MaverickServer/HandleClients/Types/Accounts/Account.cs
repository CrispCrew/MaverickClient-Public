using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer.HandleClients.Types.Accounts
{
    public class Account
    {
        int ID;
        string HWID;
        string Username;
        string LicenseKeys;
        string Products;
        string Discord;
        string Skype;

        #region Gets/Sets
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

        public string hwid
        {
            get
            {
                return HWID;
            }
            set
            {
                HWID = value;
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

        public string licensekeys
        {
            get
            {
                return LicenseKeys;
            }
            set
            {
                LicenseKeys = value;
            }
        }

        public string products
        {
            get
            {
                return Products;
            }
            set
            {
                Products = value;
            }
        }

        public string discord
        {
            get
            {
                return Discord;
            }
            set
            {
                Discord = value;
            }
        }

        public string skype
        {
            get
            {
                return Skype;
            }
            set
            {
                Skype = value;
            }
        }
        #endregion

        public Account()
        {

        }

        public Account(int ID, string HWID, string Username, string LicenseKeys, string Products, string Discord, string Skype)
        {
            this.ID = ID;
            this.HWID = HWID;
            this.Username = Username;
            this.LicenseKeys = LicenseKeys;
            this.Products = Products;
            this.Discord = Discord;
            this.Skype = Skype;
        }
    }
}
