using AuthLib.Functions.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using theme;

namespace MaverickClient
{
    public partial class Activation : Form
    {
        Client client = Login.client;
        string token = Login.Token;

        public Activation()
        {
            InitializeComponent();
        }

        private void ActivateButton_Click(object sender, EventArgs e)
        {
            string Response = client.Activate(token, LicenseKey.Text.Replace(" ", ""));

            if (Response == "Activation Successful")
            {
                // MessageBox.Show("Activation Successful");

                ActivationPopup ap = new ActivationPopup(true);
                ap.ShowDialog();

                //Handle New Product
                this.Hide();
            }
            else if (Response == "Authentication Token was not found")
            {
                // MessageBox.Show("Your login expired, please relogin!");

                ActivationPopup ap = new ActivationPopup(false);
                ap.ShowDialog();

                this.Hide();

                Login login = new Login();
                login.ShowDialog();

                return;
            }
            else
            {
                //MessageBox.Show("Unknown Response: " + Response);

                ActivationPopup ap = new ActivationPopup(false);
                ap.ShowDialog();
            }
        }

        private void flatClose1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
