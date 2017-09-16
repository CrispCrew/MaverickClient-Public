using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverickClient.Types
{
    public class News
    {
        string PostDate;
        string NewsFeed;
        int ProductId;

        #region Gets / Sets
        public string postdate
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

        public News(string postdate, string newsfeed, int productid)
        {
            this.PostDate = postdate;
            this.NewsFeed = newsfeed;
            this.ProductId = productid;
        }
    }
}
