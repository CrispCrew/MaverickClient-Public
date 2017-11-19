using AuthLib.Functions.Client;
using MaverickClient.Functions;
using System;
using System.IO;
using System.Windows.Forms;

namespace MaverickClient
{
    public partial class Register : Form
    {
        Client client = Program.client;

        public static string UsernameCache = null;
        public static string PasswordCache = null;

        public Register()
        {
            InitializeComponent();

            if (UsernameCache != null && UsernameCache != "")
                Username.Text = UsernameCache;

            if (PasswordCache != null && PasswordCache != "")
            {
                Password.PasswordChar = '*';

                Password.Text = PasswordCache;
            }

            this.LoginWarning.Hide();
        }

        private void Activate_Click(object sender, EventArgs e)
        {
            if (Password.Text != ConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
            }
            else
            {
                string resp = client.Register(Username.Text, Password.Text);
                if (resp == "Registration Successful")
                {
                    string TempToken;

                    if (client.Login(Username.Text, Password.Text, out TempToken) == "Login Found")
                    {
                        MaverickClient.Login.Token = TempToken;

                        Console.WriteLine("Login Found-" + MaverickClient.Login.Token);

                        #region AutoLogin
                        if (AutoLogin.Checked)
                        {
                            if (!File.Exists("autologin.data"))
                            {
                                StreamWriter sw = new StreamWriter("autologin.data");
                                sw.WriteLine("True");
                                sw.WriteLine(Encryption.crypt(Username.Text));
                                sw.WriteLine(Encryption.crypt(Password.Text));
                                sw.Close();
                            }
                            else
                            {
                                if (File.Exists("autologin.data"))
                                {
                                    try
                                    {
                                        File.Delete("autologin.data");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Configuration File cannot be removed");
                                    }
                                }

                                StreamWriter sw = new StreamWriter("autologin.data");
                                sw.WriteLine("True");
                                sw.WriteLine(Encryption.crypt(Username.Text));
                                sw.WriteLine(Encryption.crypt(Password.Text));
                                sw.Close();
                            }
                        }
                        else if (RememberMe.Checked)
                        {
                            if (!File.Exists("autologin.data"))
                            {
                                StreamWriter sw = new StreamWriter("autologin.data");
                                sw.WriteLine("False");
                                sw.WriteLine(Encryption.crypt(Username.Text));
                                sw.WriteLine(Encryption.crypt(Password.Text));
                                sw.Close();
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            if (File.Exists("autologin.data"))
                            {
                                try
                                {
                                    File.Delete("autologin.data");
                                }
                                catch
                                {
                                    MessageBox.Show("Configuration File cannot be removed");
                                }
                            }
                        }
                        #endregion

                        Hide();
                        new Main().ShowDialog();
                    }
                }
                else
                {
                    this.LoginWarning.Show();
                    this.LoginWarning.Text = resp;
                }
            }
        }

        private void Login_CheckedChanged(object sender)
        {
            if (Login.Checked)
            {
                Hide();
                new Login().ShowDialog();
            }
        }

        private void Username_Enter(object sender, EventArgs e)
        {
            if (Username.Text == "" || Username.Text == "Username")
            {
                Username.Text = "";
            }
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = "Username";
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            if (Password.Text == "" || Password.Text == "Password")
            {
                Password.PasswordChar = '*';
                Password.Text = "";
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (Password.Text == "")
            {
                Password.Text = "Password";
                Password.PasswordChar = '\0';
            }
        }

        private void ConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (ConfirmPassword.Text == "" || ConfirmPassword.Text == "Confirm Password")
            {
                ConfirmPassword.PasswordChar = '*';
                ConfirmPassword.Text = "";
            }
        }

        private void ConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (ConfirmPassword.Text == "")
            {
                ConfirmPassword.Text = "Confirm Password";
                ConfirmPassword.PasswordChar = '\0';
            }
        }

        private void AutoLogin_CheckedChanged(object sender)
        {
            if (AutoLogin.Checked && !RememberMe.Checked)
            {
                RememberMe.Checked = true;
            }
        }

        private void RememberMe_CheckedChanged(object sender)
        {
            if (AutoLogin.Checked && !RememberMe.Checked)
            {
                AutoLogin.Checked = false;
            }
        }

        private void formSkin1_Click(object sender, EventArgs e)
        {

        }
    }
}