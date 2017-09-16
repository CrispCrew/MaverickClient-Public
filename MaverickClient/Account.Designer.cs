namespace MaverickClient
{
    partial class Account
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Account));
            this.formSkin1 = new theme.FormSkin();
            this.CloseButton = new theme.FlatButton();
            this.LogoutButton = new theme.FlatButton();
            this.flatGroupBox1 = new theme.FlatGroupBox();
            this.flatGroupBox2 = new theme.FlatGroupBox();
            this.productInfoLabel = new theme.FlatLabel();
            this.flatGroupBox5 = new theme.FlatGroupBox();
            this.memberHWID = new theme.FlatLabel();
            this.memberName = new theme.FlatLabel();
            this.memberProducts = new theme.FlatLabel();
            this.memberDiscord = new theme.FlatLabel();
            this.memberShipID = new theme.FlatLabel();
            this.memberLicenses = new theme.FlatLabel();
            this.formSkin1.SuspendLayout();
            this.flatGroupBox1.SuspendLayout();
            this.flatGroupBox2.SuspendLayout();
            this.flatGroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.CloseButton);
            this.formSkin1.Controls.Add(this.LogoutButton);
            this.formSkin1.Controls.Add(this.flatGroupBox1);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.formSkin1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.formSkin1.HeaderMaximize = false;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(580, 406);
            this.formSkin1.TabIndex = 0;
            this.formSkin1.Text = "Maverick Cheats :: Account";
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.CloseButton.Location = new System.Drawing.Point(293, 361);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Rounded = false;
            this.CloseButton.Size = new System.Drawing.Size(109, 34);
            this.CloseButton.TabIndex = 139;
            this.CloseButton.Text = "Close";
            this.CloseButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.BackColor = System.Drawing.Color.Transparent;
            this.LogoutButton.BaseColor = System.Drawing.Color.Red;
            this.LogoutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoutButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.LogoutButton.Location = new System.Drawing.Point(178, 361);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Rounded = false;
            this.LogoutButton.Size = new System.Drawing.Size(109, 34);
            this.LogoutButton.TabIndex = 138;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // flatGroupBox1
            // 
            this.flatGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.flatGroupBox1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatGroupBox1.Controls.Add(this.flatGroupBox2);
            this.flatGroupBox1.Controls.Add(this.flatGroupBox5);
            this.flatGroupBox1.Controls.Add(this.memberLicenses);
            this.flatGroupBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flatGroupBox1.Location = new System.Drawing.Point(12, 61);
            this.flatGroupBox1.Name = "flatGroupBox1";
            this.flatGroupBox1.ShowText = true;
            this.flatGroupBox1.Size = new System.Drawing.Size(556, 298);
            this.flatGroupBox1.TabIndex = 0;
            this.flatGroupBox1.Text = "Account Info";
            // 
            // flatGroupBox2
            // 
            this.flatGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.flatGroupBox2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatGroupBox2.Controls.Add(this.productInfoLabel);
            this.flatGroupBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flatGroupBox2.Location = new System.Drawing.Point(279, 40);
            this.flatGroupBox2.Name = "flatGroupBox2";
            this.flatGroupBox2.ShowText = true;
            this.flatGroupBox2.Size = new System.Drawing.Size(241, 240);
            this.flatGroupBox2.TabIndex = 9;
            this.flatGroupBox2.Text = "Product Info";
            // 
            // productInfoLabel
            // 
            this.productInfoLabel.AutoSize = true;
            this.productInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.productInfoLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.productInfoLabel.ForeColor = System.Drawing.Color.White;
            this.productInfoLabel.Location = new System.Drawing.Point(15, 38);
            this.productInfoLabel.Name = "productInfoLabel";
            this.productInfoLabel.Size = new System.Drawing.Size(71, 13);
            this.productInfoLabel.TabIndex = 2;
            this.productInfoLabel.Text = "No products";
            // 
            // flatGroupBox5
            // 
            this.flatGroupBox5.BackColor = System.Drawing.Color.Transparent;
            this.flatGroupBox5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatGroupBox5.Controls.Add(this.memberHWID);
            this.flatGroupBox5.Controls.Add(this.memberName);
            this.flatGroupBox5.Controls.Add(this.memberProducts);
            this.flatGroupBox5.Controls.Add(this.memberDiscord);
            this.flatGroupBox5.Controls.Add(this.memberShipID);
            this.flatGroupBox5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flatGroupBox5.Location = new System.Drawing.Point(23, 40);
            this.flatGroupBox5.Name = "flatGroupBox5";
            this.flatGroupBox5.ShowText = true;
            this.flatGroupBox5.Size = new System.Drawing.Size(241, 176);
            this.flatGroupBox5.TabIndex = 8;
            this.flatGroupBox5.Text = "Membership Info";
            // 
            // memberHWID
            // 
            this.memberHWID.AutoSize = true;
            this.memberHWID.BackColor = System.Drawing.Color.Transparent;
            this.memberHWID.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberHWID.ForeColor = System.Drawing.Color.White;
            this.memberHWID.Location = new System.Drawing.Point(15, 56);
            this.memberHWID.Name = "memberHWID";
            this.memberHWID.Size = new System.Drawing.Size(88, 13);
            this.memberHWID.TabIndex = 7;
            this.memberHWID.Text = "Account HWID: ";
            // 
            // memberName
            // 
            this.memberName.AutoSize = true;
            this.memberName.BackColor = System.Drawing.Color.Transparent;
            this.memberName.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberName.ForeColor = System.Drawing.Color.White;
            this.memberName.Location = new System.Drawing.Point(15, 74);
            this.memberName.Name = "memberName";
            this.memberName.Size = new System.Drawing.Size(87, 13);
            this.memberName.TabIndex = 2;
            this.memberName.Text = "Account Name: ";
            // 
            // memberProducts
            // 
            this.memberProducts.AutoSize = true;
            this.memberProducts.BackColor = System.Drawing.Color.Transparent;
            this.memberProducts.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberProducts.ForeColor = System.Drawing.Color.White;
            this.memberProducts.Location = new System.Drawing.Point(15, 92);
            this.memberProducts.Name = "memberProducts";
            this.memberProducts.Size = new System.Drawing.Size(58, 13);
            this.memberProducts.TabIndex = 3;
            this.memberProducts.Text = "Products: ";
            // 
            // memberDiscord
            // 
            this.memberDiscord.AutoSize = true;
            this.memberDiscord.BackColor = System.Drawing.Color.Transparent;
            this.memberDiscord.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberDiscord.ForeColor = System.Drawing.Color.White;
            this.memberDiscord.Location = new System.Drawing.Point(15, 110);
            this.memberDiscord.Name = "memberDiscord";
            this.memberDiscord.Size = new System.Drawing.Size(52, 13);
            this.memberDiscord.TabIndex = 5;
            this.memberDiscord.Text = "Discord: ";
            // 
            // memberShipID
            // 
            this.memberShipID.AutoSize = true;
            this.memberShipID.BackColor = System.Drawing.Color.Transparent;
            this.memberShipID.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberShipID.ForeColor = System.Drawing.Color.White;
            this.memberShipID.Location = new System.Drawing.Point(15, 38);
            this.memberShipID.Name = "memberShipID";
            this.memberShipID.Size = new System.Drawing.Size(91, 13);
            this.memberShipID.TabIndex = 1;
            this.memberShipID.Text = "Membership ID: ";
            // 
            // memberLicenses
            // 
            this.memberLicenses.AutoSize = true;
            this.memberLicenses.BackColor = System.Drawing.Color.Transparent;
            this.memberLicenses.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.memberLicenses.ForeColor = System.Drawing.Color.White;
            this.memberLicenses.Location = new System.Drawing.Point(38, 214);
            this.memberLicenses.Name = "memberLicenses";
            this.memberLicenses.Size = new System.Drawing.Size(118, 13);
            this.memberLicenses.TabIndex = 4;
            this.memberLicenses.Text = "License Keys: (hidden)";
            this.memberLicenses.Visible = false;
            // 
            // Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 406);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Account";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Account_Load);
            this.formSkin1.ResumeLayout(false);
            this.flatGroupBox1.ResumeLayout(false);
            this.flatGroupBox1.PerformLayout();
            this.flatGroupBox2.ResumeLayout(false);
            this.flatGroupBox2.PerformLayout();
            this.flatGroupBox5.ResumeLayout(false);
            this.flatGroupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private theme.FormSkin formSkin1;
        private theme.FlatGroupBox flatGroupBox1;
        private theme.FlatLabel memberDiscord;
        private theme.FlatLabel memberLicenses;
        private theme.FlatLabel memberProducts;
        private theme.FlatLabel memberName;
        private theme.FlatLabel memberShipID;
        //private MaverickClient.FlatClose flatClose1;
        private theme.FlatLabel memberHWID;
        private theme.FlatGroupBox flatGroupBox5;
        private theme.FlatGroupBox flatGroupBox2;
        private MaverickClient.Theme.FlatClose flatClose1;
        private theme.FlatLabel productInfoLabel;
        private theme.FlatButton LogoutButton;
        private theme.FlatButton CloseButton;
    }
}