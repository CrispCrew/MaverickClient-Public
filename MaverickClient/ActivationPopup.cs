using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverickClient
{
    public partial class ActivationPopup : Form
    {
        public bool success = false;

        public ActivationPopup(bool activation_success)
        {
            InitializeComponent();
            success = activation_success;
        }

        private void ActivationPopup_Load(object sender, EventArgs e)
        {
            if (success) //If the key has been redeemed successfully
            {
                ActivationMessage.Text = "Your License-Key has been activated successfully." + Environment.NewLine + "You can now use your newly activated Cheat.";
                ActivationMessage.ForeColor = Color.FromArgb(35, 168, 109);
            }
            else //If not
            {
                ActivationMessage.Text = "There was a problem activating your License-Key." + Environment.NewLine + "Please check your code or try again later.";
                ActivationMessage.ForeColor = Color.Red;
            }
        }

        private void OkayButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
