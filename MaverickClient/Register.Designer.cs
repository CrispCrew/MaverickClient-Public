namespace MaverickClient
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.formSkin1 = new theme.FormSkin();
            this.ConfirmPassword = new System.Windows.Forms.TextBox();
            this.flatLabel1 = new theme.FlatLabel();
            this.Username = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Activate = new theme.FlatButton();
            this.Login = new theme.FlatCheckBox();
            this.flatMax1 = new theme.FlatMax();
            this.flatMini1 = new theme.FlatMini();
            this.flatClose1 = new theme.FlatClose();
            this.AutoLogin = new theme.FlatCheckBox();
            this.RememberMe = new theme.FlatCheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.LoginWarning = new theme.FlatLabel();
            this.formSkin1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.LoginWarning);
            this.formSkin1.Controls.Add(this.ConfirmPassword);
            this.formSkin1.Controls.Add(this.flatLabel1);
            this.formSkin1.Controls.Add(this.Username);
            this.formSkin1.Controls.Add(this.Password);
            this.formSkin1.Controls.Add(this.Activate);
            this.formSkin1.Controls.Add(this.Login);
            this.formSkin1.Controls.Add(this.flatMax1);
            this.formSkin1.Controls.Add(this.flatMini1);
            this.formSkin1.Controls.Add(this.flatClose1);
            this.formSkin1.Controls.Add(this.AutoLogin);
            this.formSkin1.Controls.Add(this.RememberMe);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.formSkin1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.formSkin1.HeaderMaximize = false;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Margin = new System.Windows.Forms.Padding(2);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(323, 310);
            this.formSkin1.TabIndex = 4;
            this.formSkin1.Text = "CrispyCheats:: Activate";
            // 
            // ConfirmPassword
            // 
            this.ConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.ConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfirmPassword.ForeColor = System.Drawing.Color.Silver;
            this.ConfirmPassword.Location = new System.Drawing.Point(106, 140);
            this.ConfirmPassword.Name = "ConfirmPassword";
            this.ConfirmPassword.Size = new System.Drawing.Size(127, 22);
            this.ConfirmPassword.TabIndex = 3;
            this.ConfirmPassword.Text = "Confirm Password";
            this.ConfirmPassword.Enter += new System.EventHandler(this.ConfirmPassword_Enter);
            this.ConfirmPassword.Leave += new System.EventHandler(this.ConfirmPassword_Leave);
            // 
            // flatLabel1
            // 
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.flatLabel1.ForeColor = System.Drawing.Color.White;
            this.flatLabel1.Location = new System.Drawing.Point(101, 51);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(99, 30);
            this.flatLabel1.TabIndex = 126;
            this.flatLabel1.Text = "Activate";
            // 
            // Username
            // 
            this.Username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.Username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Username.ForeColor = System.Drawing.Color.Silver;
            this.Username.Location = new System.Drawing.Point(106, 84);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(127, 22);
            this.Username.TabIndex = 1;
            this.Username.Text = "Username";
            this.Username.Enter += new System.EventHandler(this.Username_Enter);
            this.Username.Leave += new System.EventHandler(this.Username_Leave);
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Password.ForeColor = System.Drawing.Color.Silver;
            this.Password.Location = new System.Drawing.Point(106, 112);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(127, 22);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            this.Password.Enter += new System.EventHandler(this.Password_Enter);
            this.Password.Leave += new System.EventHandler(this.Password_Leave);
            // 
            // Activate
            // 
            this.Activate.BackColor = System.Drawing.Color.Transparent;
            this.Activate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.Activate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Activate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Activate.Location = new System.Drawing.Point(106, 246);
            this.Activate.Name = "Activate";
            this.Activate.Rounded = false;
            this.Activate.Size = new System.Drawing.Size(92, 29);
            this.Activate.TabIndex = 6;
            this.Activate.Text = "Activate";
            this.Activate.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Activate.Click += new System.EventHandler(this.Activate_Click);
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.Login.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.Login.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.Login.Checked = false;
            this.Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Login.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Login.Location = new System.Drawing.Point(106, 218);
            this.Login.Name = "Login";
            this.Login.Options = theme.FlatCheckBox._Options.Style1;
            this.Login.Size = new System.Drawing.Size(127, 22);
            this.Login.TabIndex = 5;
            this.Login.Text = "Login";
            this.Login.CheckedChanged += new theme.FlatCheckBox.CheckedChangedEventHandler(this.Login_CheckedChanged);
            // 
            // flatMax1
            // 
            this.flatMax1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatMax1.BackColor = System.Drawing.Color.White;
            this.flatMax1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatMax1.Font = new System.Drawing.Font("Marlett", 12F);
            this.flatMax1.Location = new System.Drawing.Point(281, 10);
            this.flatMax1.Margin = new System.Windows.Forms.Padding(2);
            this.flatMax1.Name = "flatMax1";
            this.flatMax1.Size = new System.Drawing.Size(18, 18);
            this.flatMax1.TabIndex = 125;
            this.flatMax1.Text = "flatMax1";
            this.flatMax1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // flatMini1
            // 
            this.flatMini1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatMini1.BackColor = System.Drawing.Color.White;
            this.flatMini1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatMini1.Font = new System.Drawing.Font("Marlett", 12F);
            this.flatMini1.Location = new System.Drawing.Point(261, 10);
            this.flatMini1.Margin = new System.Windows.Forms.Padding(2);
            this.flatMini1.Name = "flatMini1";
            this.flatMini1.Size = new System.Drawing.Size(18, 18);
            this.flatMini1.TabIndex = 124;
            this.flatMini1.Text = "flatMini1";
            this.flatMini1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // flatClose1
            // 
            this.flatClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatClose1.BackColor = System.Drawing.Color.White;
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatClose1.Font = new System.Drawing.Font("Marlett", 10F);
            this.flatClose1.Location = new System.Drawing.Point(300, 10);
            this.flatClose1.Margin = new System.Windows.Forms.Padding(2);
            this.flatClose1.Name = "flatClose1";
            this.flatClose1.Size = new System.Drawing.Size(18, 18);
            this.flatClose1.TabIndex = 123;
            this.flatClose1.Text = "flatClose1";
            this.flatClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // AutoLogin
            // 
            this.AutoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.AutoLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.AutoLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.AutoLogin.Checked = false;
            this.AutoLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AutoLogin.Location = new System.Drawing.Point(106, 193);
            this.AutoLogin.Name = "AutoLogin";
            this.AutoLogin.Options = theme.FlatCheckBox._Options.Style1;
            this.AutoLogin.Size = new System.Drawing.Size(127, 22);
            this.AutoLogin.TabIndex = 4;
            this.AutoLogin.Text = "Auto Login";
            this.AutoLogin.CheckedChanged += new theme.FlatCheckBox.CheckedChangedEventHandler(this.AutoLogin_CheckedChanged);
            // 
            // RememberMe
            // 
            this.RememberMe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.RememberMe.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.RememberMe.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.RememberMe.Checked = false;
            this.RememberMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RememberMe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RememberMe.Location = new System.Drawing.Point(106, 168);
            this.RememberMe.Name = "RememberMe";
            this.RememberMe.Options = theme.FlatCheckBox._Options.Style1;
            this.RememberMe.Size = new System.Drawing.Size(127, 22);
            this.RememberMe.TabIndex = 3;
            this.RememberMe.Text = "Remember Me";
            // 
            // LoginWarning
            // 
            this.LoginWarning.BackColor = System.Drawing.Color.Red;
            this.LoginWarning.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginWarning.ForeColor = System.Drawing.Color.White;
            this.LoginWarning.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoginWarning.Location = new System.Drawing.Point(0, 280);
            this.LoginWarning.Name = "LoginWarning";
            this.LoginWarning.Size = new System.Drawing.Size(323, 30);
            this.LoginWarning.TabIndex = 130;
            this.LoginWarning.Text = "Login Failed";
            this.LoginWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 310);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.formSkin1.ResumeLayout(false);
            this.formSkin1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private theme.FormSkin formSkin1;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private theme.FlatButton Activate;
        private theme.FlatCheckBox Login;
        private theme.FlatMax flatMax1;
        private theme.FlatMini flatMini1;
        private theme.FlatClose flatClose1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private theme.FlatLabel flatLabel1;
        private System.Windows.Forms.TextBox ConfirmPassword;
        private theme.FlatCheckBox AutoLogin;
        private theme.FlatCheckBox RememberMe;
        private theme.FlatLabel LoginWarning;
    }
}