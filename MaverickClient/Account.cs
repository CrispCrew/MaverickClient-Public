using AuthLib.Functions.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverickClient
{
    public partial class Account : Form
    {
        public static Client client = Login.client;

        public string Token;
        private List<Product> products;
        private string Licenses = "";

        public Account(string Token, List<Product> products_list)
        {
            InitializeComponent();

            this.Token = Token;
            this.products = products_list;
        }
        
        #region Form Handlers
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Account_Load(object sender, EventArgs e)
        {
            //Account Details
            string AccountDetails = client.AccountDetails(Token);

            //AccountDetails is an array
            if (AccountDetails.Contains("|"))
            {
                string[] AccountInfo = AccountDetails.Split('|');

                Console.WriteLine("Account Details: " + AccountDetails);
                
                this.memberShipID.Text += AccountInfo[0];
                this.memberName.Text += AccountInfo[1];
                this.memberHWID.Text += AccountInfo[2];
                this.memberDiscord.Text += AccountInfo[3];
                if (AccountInfo.Length > 5) { this.memberProducts.Text += AccountInfo[5].Replace("1", "GTA5").Replace("2", " CS:GO"); } else { this.memberProducts.Text += "-"; }
                if (AccountInfo.Length > 8) { this.Licenses += AccountInfo[8]; } else { this.Licenses += "-"; }
            }
            else
            {
                this.memberShipID.Text = "A Error has occured";
                this.memberHWID.Text = "A Error has occured";
                this.memberName.Text = "A Error has occured";
                this.memberLicenses.Text = "A Error has occured";
                this.memberProducts.Text = "A Error has occured";
                this.memberDiscord.Text = "A Error has occured";
            }
            
            //Products
            string owned = client.Products(Token).Replace("Packages=", "");
            if (owned != "" && owned != "API Quota Reached")
            {
                if (!owned.Contains("|"))
                {
                    productInfoLabel.Text = "";

                    string[] product_details = owned.Split('-');

                    int Id = Convert.ToInt32(product_details[0]); //UID
                    string Name = product_details[1]; //Product Name
                    string File = product_details[2]; //Product Media
                    int Status = Convert.ToInt32(product_details[3]); //Product Status
                    int Version = Convert.ToInt32(product_details[4]);

                    productInfoLabel.Text += "Product Information - " + Name + Environment.NewLine;
                    productInfoLabel.Text += "ID: " + Id + Environment.NewLine;
                    productInfoLabel.Text += "Version: v" + Version + Environment.NewLine;
                    productInfoLabel.Text += "Name: " + Name + Environment.NewLine;
                    productInfoLabel.Text += "File: " + File + Environment.NewLine;
                    productInfoLabel.Text += "Status: " + Status + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    productInfoLabel.Text = "";

                    foreach (string product in owned.Split('|'))
                    {
                        string[] product_details = product.Split('-');

                        int Id = Convert.ToInt32(product_details[0]); //UID
                        string Name = product_details[1]; //Product Name
                        string File = product_details[2]; //Product Media
                        int Status = Convert.ToInt32(product_details[3]); //Product Status
                        int Version = Convert.ToInt32(product_details[4]);

                        productInfoLabel.Text += "Product Information - " + Name + Environment.NewLine;
                        productInfoLabel.Text += "ID: " + Id + Environment.NewLine;
                        productInfoLabel.Text += "Version: v" + Version + Environment.NewLine;
                        productInfoLabel.Text += "Name: " + Name + Environment.NewLine;
                        productInfoLabel.Text += "File: " + File + Environment.NewLine;
                        productInfoLabel.Text += "Status: " + Status + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            else if (owned == "API Quota Reached")
            {
                MessageBox.Show("Your IP has been temporarily banned as a security percaution, try again in a few minutes!");
            }
            else
            {
                Console.WriteLine("You do not own any cheats, please activate some!");
            }
            
        }

        /// <summary>
        /// Hide Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flatClose1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Minimize Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flatMini1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            if (File.Exists("autologin.data"))
            {
                File.Delete("autologin.data"); //Delete Autologin

                //Restart Application for compatibility
                System.Diagnostics.Process.Start(Application.ExecutablePath); //Start another process
                Environment.Exit(0); //Close this process
            }
            else
            {

                //Restart Application for compatibility
                System.Diagnostics.Process.Start(Application.ExecutablePath); //Start another process
                Environment.Exit(0); //Close this process
            }
        }
        /// <summary>
        /// Close Account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.memberLicenses.Text = "License Keys: (hidden)";
            this.Hide();
        }
        #endregion

        private void showLicenses_Click(object sender, EventArgs e)
        {
            if (this.memberLicenses.Text == "License Keys: (hidden)")
            {
                this.memberLicenses.Text = "License Keys: " + Licenses;
            }
            else
            {
                this.memberLicenses.Text = "License Keys: (hidden)";
            }
        }
    }
}
