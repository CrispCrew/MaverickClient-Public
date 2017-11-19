using System.Data.SqlClient;

namespace DiscordBot
{
    public class Product
    {
        int Id; //UID
        string Name; //Product Name
        string File; //Product Media
        string ProcessName;
        int Status; //Product Status
        int Version; //Product Version
        int Free;
        long AutoLaunchMemory;

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

        public int free
        {
            get
            {
                return Free;
            }
            set
            {
                Free = value;
            }
        }

        public long autolaunchmemory
        {
            get
            {
                return AutoLaunchMemory;
            }
            set
            {
                AutoLaunchMemory = value;
            }
        }
        #endregion

        //ServerResponse += product.id + ":" + product.name + ":" + product.file + ":" + product.processname + ":" + product.status + ":" + product.version + ":" + product.free + ":" + product.discordperm + ":" + product.autolaunchmem;
        public Product(int Id, string Name, string File, string ProcessName, int Status, int Version, int Free, long AutoLaunchMemory)
        {
            this.Id = Id;
            this.Name = Name;
            this.File = File;
            this.ProcessName = ProcessName;
            this.Status = Status;
            this.Version = Version;
            this.Free = Free;
            this.AutoLaunchMemory = AutoLaunchMemory;
        }

        public string StatusIDToString()
        {
            if (this.Status == 1)
            {
                return "Undetected";
            }
            else if (this.Status == -1)
            {
                return "Detected";
            }
            else if (this.Status == 0)
            {
                return "Updating";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
