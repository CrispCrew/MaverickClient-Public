using System;
using System.Data.SqlClient;

namespace MaverickClient
{
    public class Product
    {
        int ID; //UID
        string Name; //Product Name
        string File; //Product Media
        string ProcessName; //Product Proc Name
        int Status; //Product Status
        int Version; //Product Version
        long AutoLaunch; //Product AutoLaunch

        #region Gets / Sets
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

        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        public string file
        {
            get
            {
                return File;
            }
            set
            {
                File = value;
            }
        }

        public string processname
        {
            get
            {
                return ProcessName;
            }
            set
            {
                ProcessName = value;
            }
        }

        public int status
        {
            get
            {
                return Status;
            }
            set
            {
                Status = value;
            }
        }

        public int version
        {
            get
            {
                return Version;
            }
            set
            {
                Version = value;
            }
        }

        public long autolaunch
        {
            get
            {
                return AutoLaunch;
            }
            set
            {
                AutoLaunch = value;
            }
        }
        #endregion

        public Product(int ID, string Name, string ProcessName, string File, int Status, int Version, long AutoLaunch)
        {
            this.ID = ID;
            this.Name = Name;
            this.File = File;
            this.ProcessName = ProcessName;
            this.Status = Status;
            this.Version = Version;
            this.AutoLaunch = AutoLaunch;
        }

        public string ProductStatus()
        {
            if (Status == -1)
            {
                return "Offline";
            }
            else if (Status == 0)
            {
                return "Updating";
            }
            else if (Status == 1)
            {
                return "Undetected";
            }
            else
            {
                return "Unknown";
            }
        }

        public static string ProductStatus(int Status)
        {
            if (Status == -1)
            {
                return "Offline";
            }
            else if (Status == 0)
            {
                return "Updating";
            }
            else if (Status == 1)
            {
                return "Undetected";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
