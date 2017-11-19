using MaverickServer.HandleClients;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer
{
    public class Product
    {
        int Id; //UID
        int Group; //UID
        string Name; //Product Name
        string File; //Product Media
        string ProcessName; //Product ProcessName
        int Status; //Product Status
        int Version; //Product Version
        int Free; //Product Free [0=no][1=yes]
        long AutoLaunchMem;

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

        public int group
        {
            get
            {
                return Group;
            }
            set
            {
                Group = value;
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

        public long autolaunchmem
        {
            get
            {
                return AutoLaunchMem;
            }
            set
            {
                AutoLaunchMem = value;
            }
        }
        #endregion

        public Product()
        {
            
        }

        public Product(int Id, int Group, string Name, string File, string ProcessName, int Status, int Version, int Free)
        {
            this.Id = Id;
            this.Group = Group;
            this.Name = Name;
            this.File = File;
            this.ProcessName = ProcessName;
            this.Status = Status;
            this.Version = Version;
            this.Free = Free;
        }

        public void SetFromSQL(MySqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Group = reader.GetInt32(1);
            Name = reader.GetString(2);
            File = reader.GetString(3);
            ProcessName = reader.GetString(4);
            Status = reader.GetInt32(5);
            Version = reader.GetInt32(6);
            Free = reader.GetInt32(7);
            AutoLaunchMem = reader.GetInt64(8);
        }
    }
}
