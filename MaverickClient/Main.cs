using AuthLib.Functions.Client;
using MaverickClient.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverickClient
{
    public partial class Main : Form
    {
        public static string Version = Login.LocalVersion;

        List<Product> products = new List<Product>();
        List<News> newsfeed = new List<News>();

        public static Client client = Login.client;
        public static string Token = "";
        
        public Main()
        {
            InitializeComponent();
        }

        private void ProcessScan()
        {
            bool notlaunched = true;

            while (notlaunched)
            {
                foreach (Product product in new List<Product>(products))
                {
                    Process[] processlist = Process.GetProcessesByName(product.name);

                    if (processlist.Length > 0)
                    {
                        int productid = product.id;
                        string productname = product.name;
                        string productfile = product.file;

                        if (productid <= 0 || productname == null || productfile == null)
                            return;

                        if (File.Exists(Environment.CurrentDirectory + "\\" + productfile))
                        {
                            try
                            {
                                File.Delete(Environment.CurrentDirectory + "\\" + productfile);
                            }
                            catch
                            {

                            }
                        }

                        if (Directory.Exists(Environment.CurrentDirectory + "\\" + productname + "\\"))
                        {
                            try
                            {
                                Directory.Delete(Environment.CurrentDirectory + "\\" + productname + "\\");
                            }
                            catch
                            {

                            }
                        }

                        string download = client.Download(Token, productfile, productid);

                        if (download == "Download Completed")
                        {
                            if (File.Exists(Environment.CurrentDirectory + "\\" + productfile) && CheckZIP(Environment.CurrentDirectory + "\\" + productfile))
                            {
                                if (!Directory.Exists(Environment.CurrentDirectory + "\\" + productname + "\\"))
                                {
                                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                                }
                                else
                                {
                                    try
                                    {
                                        Directory.Delete(Environment.CurrentDirectory + "\\" + productname + "\\", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error: Cheat Files may be in use, ensure the cheat is not already open!");
                                        Console.WriteLine(ex.ToString());
                                    }

                                    Thread.Sleep(500);

                                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                                }

                                ZipArchive archive = ZipFile.Open(Environment.CurrentDirectory + "\\" + productfile, ZipArchiveMode.Read);
                                archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                                archive.Dispose();

                                if (File.Exists(Environment.CurrentDirectory + "\\" + productname + "\\run.exe"))
                                {
                                    Console.WriteLine("Cheat Preloaded - Waiting 120s to Auto Inject");

                                    bool exit = false;

                                    Task.Factory.StartNew(() =>
                                    {
                                        while (Console.ReadKey().Key != ConsoleKey.Escape) ;
                                        exit = true;
                                    });

                                    //Check if Process has loaded enough memory that we can likely load successfuly
                                    int secs = 0;
                                    while (!exit && secs < 120)
                                    {
                                        Process gameproc = Process.GetProcessById(processlist[0].Id);

                                        if (gameproc.PeakWorkingSet64 > product.autolaunch)
                                        {
                                            Console.WriteLine("Process reached trigger memory count, skipping timer!");

                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Name: " + gameproc.ProcessName + " PID: " + gameproc.Id + "-" + gameproc.PeakWorkingSet64 + "<" + product.autolaunch);
                                        }

                                        Console.WriteLine("Cheat Launching in {0}s/120s", secs);

                                        Thread.Sleep(1000);

                                        secs++;
                                    }

                                    Console.WriteLine("Cheat Launching!");

                                    Process process = new Process();
                                    ProcessStartInfo startInfo = new ProcessStartInfo()
                                    {
                                        WorkingDirectory = Environment.CurrentDirectory + "\\" + productname + "\\",
                                        //UseShellExecute = false,
                                        FileName = Environment.CurrentDirectory + "\\" + productname + "\\run.exe",
                                        //WindowStyle = ProcessWindowStyle.Hidden,
                                        //CreateNoWindow = true,
                                        UseShellExecute = true,
                                        Verb = "runas"
                                    };
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }
                            }
                        }
                        else if (download == "Authentication Token was not found")
                        {
                            MessageBox.Show("Your login expired, please relogin!");

                            this.Hide();

                            Login login = new Login();
                            login.ShowDialog();

                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error: Files failed to be downloaded!");
                        }

                        notlaunched = false;

                        break;
                    }
                }

                Thread.Sleep(1000);
            }
        } 

        private void Main_Load(object sender, EventArgs e)
        {
            Token = Login.Token;

            LoaderVersion.Text = "Loader Version: " + Version;

            //Products
            string owned = client.Products(Token).Replace("Packages=", "");
            if (owned != "")
            {
                if (!owned.Contains("|"))
                {
                    string[] product_details = owned.Split('-');

                    int Id = Convert.ToInt32(product_details[0]); //UID
                    string Name = product_details[1]; //Product Name
                    string File = product_details[2]; //Product Media
                    int Status = Convert.ToInt32(product_details[3]); //Product Status
                    int Version = Convert.ToInt32(product_details[4]);
                    long AutoLaunch = Convert.ToInt64(product_details[5]);

                    products.Add(new Product(Id, Name, File, Status, Version, AutoLaunch));
                }
                else
                {
                    foreach (string product in owned.Split('|'))
                    {
                        string[] product_details = product.Split('-');

                        int Id = Convert.ToInt32(product_details[0]); //UID
                        string Name = product_details[1]; //Product Name
                        string File = product_details[2]; //Product Media
                        int Status = Convert.ToInt32(product_details[3]); //Product Status
                        int Version = Convert.ToInt32(product_details[4]);
                        long AutoLaunch = Convert.ToInt64(product_details[5]);

                        products.Add(new Product(Id, Name, File, Status, Version, AutoLaunch));
                    }
                }
            }
            else
            {
                Console.WriteLine("You do not own any cheats, please activate some!");
            }

            //Newsfeed
            string productnewsfeed = client.NewsFeed(Token).Replace("Newsfeed=", "");
            if (productnewsfeed != "")
            {
                if (!productnewsfeed.Contains("|"))
                {
                    string[] productnews = productnewsfeed.Split('-');

                    int ProductId = Convert.ToInt32(productnews[0]);
                    string NewsFeed = productnews[1];
                    string PostedDate = productnews[2];

                    newsfeed.Add(new News(PostedDate, NewsFeed, ProductId));
                }
                else
                {
                    string[] newsfeeds = productnewsfeed.Split('|');

                    foreach (string feed in newsfeeds)
                    {
                        string[] productnews = feed.Split('-');

                        int ProductId = Convert.ToInt32(productnews[0]);
                        string NewsFeed = productnews[1];
                        string PostedDate = productnews[2];

                        newsfeed.Add(new News(PostedDate, NewsFeed, ProductId));
                    }
                }
            }
            else
            {
                Console.WriteLine("There's no news at this moment!");
            }

            foreach (Product product in products)
            {
                flatComboBox1.Items.Add(product.name + " [ " + Product.ProductStatus(product.status) + " ] ");
            }

            new Thread(ProcessScan).Start();
        }

        /// <summary>
        /// Product List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearNewsfeed();

            Product selectedproduct = null;

            foreach (Product product in products)
            {
                if (flatComboBox1.Text.Contains(product.name))
                {
                    selectedproduct = product;

                    cheatVersionLabel.Text = "Cheat Version: " + product.version;

                    break; //Break to save performance
                }
            }

            if (selectedproduct != null)
            {
                foreach (News news in newsfeed)
                {
                    if (news.productid == selectedproduct.id)
                    {
                        Console.WriteLine("ProductID found: " + news.productid + "->" + selectedproduct.id);

                        flatTextBox1.Text += Environment.NewLine + news.postdate.ToString() + " - " + news.newsfeed;
                    }
                }
            }
        }

        /// <summary>
        /// Account Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Account_Click(object sender, EventArgs e)
        {
            //Show Account Form
            Account account = new Account(Token, products);
            account.ShowDialog();
        }

        /// <summary>
        /// Launch Cheat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flatButton2_Click(object sender, EventArgs e)
        {
            int productid = 0;
            string productname = null;
            string productfile = null;

            foreach (Product product in products)
            {
                if (flatComboBox1.Text.Contains(product.name))
                {
                    productid = product.id;
                    productname = product.name;
                    productfile = product.file;

                    break;
                }
            }

            if (productid <= 0 || productname == null ||productfile == null)
                return;

            if (File.Exists(Environment.CurrentDirectory + "\\" + productfile))
            {
                try
                {
                    File.Delete(Environment.CurrentDirectory + "\\" + productfile);
                }
                catch
                {

                }
            }

            if (Directory.Exists(Environment.CurrentDirectory + "\\" + productname + "\\"))
            {
                try
                {
                    Directory.Delete(Environment.CurrentDirectory + "\\" + productname + "\\");
                }
                catch
                {

                }
            }

            string download = client.Download(Token, productfile, productid);

            if (download == "Download Completed")
            {
                if (File.Exists(Environment.CurrentDirectory + "\\" + productfile) && CheckZIP(Environment.CurrentDirectory + "\\" + productfile))
                {
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\" + productname + "\\"))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                    }
                    else
                    {
                        try
                        {
                            Directory.Delete(Environment.CurrentDirectory + "\\" + productname + "\\", true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: Cheat Files may be in use, ensure the cheat is not already open!");
                            Console.WriteLine(ex.ToString());
                        }

                        Thread.Sleep(500);

                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                    }

                    ZipArchive archive = ZipFile.Open(Environment.CurrentDirectory + "\\" + productfile, ZipArchiveMode.Update);
                    archive.ExtractToDirectory(Environment.CurrentDirectory + "\\" + productname + "\\");
                    archive.Dispose();

                    if (File.Exists(Environment.CurrentDirectory + "\\" + productname + "\\run.exe"))
                    {
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo()
                        {
                            WorkingDirectory = Environment.CurrentDirectory + "\\" + productname + "\\",
                            //UseShellExecute = false,
                            FileName = Environment.CurrentDirectory + "\\" + productname + "\\run.exe",
                            //WindowStyle = ProcessWindowStyle.Hidden,
                            //CreateNoWindow = true,
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                }
            }
            else if (download == "Authentication Token was not found")
            {
                MessageBox.Show("Your login expired, please relogin!");

                this.Hide();

                Login login = new Login();
                login.ShowDialog();

                return;
            }
            else
            {
                MessageBox.Show("Error: Files failed to be downloaded!");
            }
        }

        /// <summary>
        /// Cleans NewsFeed and sets default style
        /// </summary>
        void ClearNewsfeed()
        {
            flatTextBox1.Text = "Newsfeed" + Environment.NewLine;
        }

        /// <summary>
        /// Checks ZIP File Status
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool CheckZIP(string file)
        {
            try
            {
                ZipArchive archive = ZipFile.Open(file, ZipArchiveMode.Read);
                archive.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ZIP Corrupted");
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// Activate product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flatButton1_Click(object sender, EventArgs e)
        {
            //Always 0
            List<Product> temp = new List<Product>();

            string SelectedProduct = flatComboBox1.Text;

            Console.WriteLine("Selected Product: " + SelectedProduct);

            new Activation().ShowDialog();

            products.Clear();
            newsfeed.Clear();

            //Products
            string owned = client.Products(Token).Replace("Packages=", "");
            if (owned != "" && owned != "Authentication Token was not found")
            {
                if (!owned.Contains("|"))
                {
                    string[] product_details = owned.Split('-');

                    int Id = Convert.ToInt32(product_details[0]); //UID
                    string Name = product_details[1]; //Product Name
                    string File = product_details[2]; //Product Media
                    int Status = Convert.ToInt32(product_details[3]); //Product Status
                    int Version = Convert.ToInt32(product_details[4]);
                    long AutoLaunch = Convert.ToInt64(product_details[5]);

                    products.Add(new Product(Id, Name, File, Status, Version, AutoLaunch));
                }
                else
                {
                    foreach (string product in owned.Split('|'))
                    {
                        string[] product_details = product.Split('-');

                        int Id = Convert.ToInt32(product_details[0]); //UID
                        string Name = product_details[1]; //Product Name
                        string File = product_details[2]; //Product Media
                        int Status = Convert.ToInt32(product_details[3]); //Product Status
                        int Version = Convert.ToInt32(product_details[4]);
                        long AutoLaunch = Convert.ToInt64(product_details[5]);

                        products.Add(new Product(Id, Name, File, Status, Version, AutoLaunch));
                    }
                }
            }
            else if (owned == "Authentication Token was not found")
            {
                MessageBox.Show("Your login expired, please relogin!");

                this.Hide();

                Login login = new Login();
                login.ShowDialog();

                return;
            }
            else
            {
                Console.WriteLine("You do not own any cheats, please activate some!");
            }

            //Newsfeed
            string productnewsfeed = client.NewsFeed(Token).Replace("Newsfeed=", "");

            if (productnewsfeed != "" && productnewsfeed != "Authentication Token was not found")
            {
                if (!productnewsfeed.Contains("|"))
                {
                    string[] productnews = productnewsfeed.Split('-');

                    int ProductId = Convert.ToInt32(productnews[0]);
                    string NewsFeed = productnews[1];
                    string PostedDate = productnews[2];

                    newsfeed.Add(new News(PostedDate, NewsFeed, ProductId));
                }
                else
                {
                    string[] newsfeeds = productnewsfeed.Split('|');

                    foreach (string feed in newsfeeds)
                    {
                        string[] productnews = feed.Split('-');

                        int ProductId = Convert.ToInt32(productnews[0]);
                        string NewsFeed = productnews[1];
                        string PostedDate = productnews[2];

                        newsfeed.Add(new News(PostedDate, NewsFeed, ProductId));
                    }
                }
            }

            flatComboBox1.Items.Clear();

            foreach (Product product in products)
            {
                flatComboBox1.Items.Add(product.name + " [ " + Product.ProductStatus(product.status) + " ] ");
            }

            Console.WriteLine(temp.Count + " - " + products.Count);

            if (temp.Count == 0 && products.Count > 0)
            {
                SelectedProduct = products[0].name + " [ " + Product.ProductStatus(products[0].status) + " ] ";

                Console.WriteLine("Selected Product: " + SelectedProduct);
            }

            Console.WriteLine(flatComboBox1.Items.Contains(SelectedProduct) ? "true" : "false");

            flatComboBox1.SelectedIndex = flatComboBox1.Items.IndexOf(SelectedProduct);
        }

        /// <summary>
        /// Opens the credits
        /// </summary>
        /// <returns></returns>
        private void CreditsButton_Click(object sender, EventArgs e)
        {
            Credits credits = new Credits();
            credits.ShowDialog();
        }
    }
}