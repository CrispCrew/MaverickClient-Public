using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickServer
{
    public class News
    {
        int ProductId;
        string NewsFeed;
        DateTime PostDate;

        #region Gets / Sets
        public DateTime postdate
        {
            get
            {
                return PostDate;
            }
            set
            {
                PostDate = value;
            }
        }

        public string newsfeed
        {
            get
            {
                return NewsFeed;
            }
            set
            {
                NewsFeed = value;
            }
        }

        public int productid
        {
            get
            {
                return ProductId;
            }
            set
            {
                ProductId = value;
            }
        }
        #endregion

        public News()
        {
            
        }

        public News(MySqlDataReader reader)
        {
            ProductId = reader.GetInt32(1);
            NewsFeed = reader.GetString(2);
            PostDate = reader.GetDateTime(3);
        }

        public News(DateTime postdate, string newsfeed, int productid)
        {
            this.ProductId = productid;
            this.NewsFeed = newsfeed;
            this.PostDate = postdate;
        }
    }
}
