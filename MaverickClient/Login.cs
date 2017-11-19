using System;
using System.Windows.Forms;
using AuthLib.Functions.Client;
using System.IO;
using MaverickClient.Functions;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace MaverickClient
{
    public partial class Login : Form
    {
        public static string LocalVersion = "0.41";

        public static Client client = Program.client;

        public static string Token;

        public Login()
        {
            InitializeComponent();

            string ServerVersion = client.Version();

            Console.WriteLine("Local Version: " + LocalVersion + " - Server Version: " + ServerVersion);

            if (ServerVersion != "API Quota Reached" && ServerVersion != "Connection Quota Reached")
            {
                if (LocalVersion != ServerVersion)
                {
                    MessageBox.Show("Updating");

                    retry:
                    //Run AutoUpdater
                    if (File.Exists(Environment.CurrentDirectory + "\\Updater.exe"))
                    {
                        Console.WriteLine("Updater Executable Found!");

                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo()
                        {
                            WorkingDirectory = Environment.CurrentDirectory + "\\",
                            //UseShellExecute = false,
                            FileName = Environment.CurrentDirectory + "\\" + "Updater.exe",
                            //WindowStyle = ProcessWindowStyle.Hidden,
                            //CreateNoWindow = true,
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    else
                    {
                        Console.WriteLine("AutoUpdater Executable Missing! -> Recovery: Trying to download AutoUpdater");

                        new WebClient().DownloadFile("https://maverickcheats.eu/downloads/Updater.exe", Environment.CurrentDirectory + "\\Updater.exe");

                        goto retry;
                    }
                    
                    Environment.Exit(0);
                }             
            }
            else
            {
                MessageBox.Show("Your IP has been temporarily banned as a security percaution, try again in a few minutes!");
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string TempToken;

            string resp = client.Login(Username.Text, Password.Text, out TempToken);

            if (resp == "Login Found")
            {
                Token = TempToken;

                Console.WriteLine("Login Found-" + Token);

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
            else if (resp == "Password Reset")
            {
                Console.Write("New Password: ");

                //Show Reset Form
                string NewPassword = Console.ReadLine();

                Console.Write("Confirm Password: ");

                string ConfirmPassword = Console.ReadLine();

                if (NewPassword == ConfirmPassword)
                    client.ResetPassword(Username.Text, NewPassword);
            }
            else
            {
                this.LoginWarning.Visible = true;
                this.LoginWarning.Text = resp;
            }
        }

        private void Register_CheckedChanged(object sender)
        {
            if (Register.Checked)
            {
                if (Username.Text != null && Username.Text != "")
                    MaverickClient.Register.UsernameCache = Username.Text;

                if (Password.Text != null && Password.Text != "")
                    MaverickClient.Register.PasswordCache = Password.Text;

                Hide();
                new Register().ShowDialog();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (File.Exists("autologin.data"))
            {
                //Load Config
                StreamReader sr = new StreamReader("autologin.data");
                string useAutoLogin = sr.ReadLine();
                string userHash = sr.ReadLine();
                string pwHash = sr.ReadLine();

                sr.Close();
                sr.Dispose();

                if (Convert.ToBoolean(useAutoLogin))
                {
                    //make sure the user cant see the password
                    Password.PasswordChar = '*';

                    //Setup Decryption...etc
                    RememberMe.Checked = true;
                    AutoLogin.Checked = true;
                    userHash = Encryption.decrypt(userHash);
                    pwHash = Encryption.decrypt(pwHash);
                    Username.Text = userHash;
                    Password.Text = pwHash;

                    string TempToken;

                    string resp = client.Login(Username.Text, Password.Text, out TempToken);

                    if (resp == "Login Found")
                    {
                        Token = TempToken;

                        Console.WriteLine("Login Found-" + Token);

                        this.Hide();

                        new Main().ShowDialog();
                    }
                    else if (resp == "Password Reset")
                    {
                        Console.Write("New Password: ");

                        //Show Reset Form
                        string NewPassword = Console.ReadLine();

                        client.ResetPassword(Username.Text, NewPassword);
                    }
                    else
                    {
                        this.LoginWarning.Visible = true;
                        this.LoginWarning.Text = resp;
                    }
                }
                else
                {
                    RememberMe.Checked = true;
                    userHash = Encryption.decrypt(userHash);
                    pwHash = Encryption.decrypt(pwHash);
                    Username.Text = userHash;
                    Password.Text = pwHash;
                }
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
    }
}
