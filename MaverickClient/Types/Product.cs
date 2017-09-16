using System;
using System.Data.SqlClient;

namespace MaverickClient
{
    public class Product
    {
        int Id; //UID
        string Name; //Product Name
        string File; //Product Media
        int Status; //Product Status
        int Version; //Product Version
        long AutoLaunch;

        #region Gets / Sets
        public int id
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
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

        public Product(int id, string name, string file, int status, int version, long autolaunch)
        {
            this.Id = id;
            this.Name = name;
            this.File = file;
            this.Status = status;
            this.Version = version;
            this.AutoLaunch = autolaunch;
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
