namespace MaverickServer
{
    partial class Server
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
            this.components = new System.ComponentModel.Container();
            this.formSkin1 = new theme.FormSkin();
            this.flatClose1 = new MaverickClient.Theme.FlatClose();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flatComboBox1 = new theme.FlatComboBox();
            this.gridView2Strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.assoicatedAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownedLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1Strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.formSkin1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.gridView2Strip.SuspendLayout();
            this.gridView1Strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.flatClose1);
            this.formSkin1.Controls.Add(this.gridControl2);
            this.formSkin1.Controls.Add(this.flatComboBox1);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.formSkin1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.formSkin1.HeaderMaximize = false;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(1189, 836);
            this.formSkin1.TabIndex = 0;
            this.formSkin1.Text = "Maverick Loader :: Server";
            // 
            // flatClose1
            // 
            this.flatClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatClose1.BackColor = System.Drawing.Color.White;
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.flatClose1.Font = new System.Drawing.Font("Marlett", 10F);
            this.flatClose1.Location = new System.Drawing.Point(1159, 12);
            this.flatClose1.Name = "flatClose1";
            this.flatClose1.Size = new System.Drawing.Size(18, 18);
            this.flatClose1.TabIndex = 6;
            this.flatClose1.Text = "flatClose1";
            this.flatClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.flatClose1.Click += new System.EventHandler(this.flatClose1_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(12, 115);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1165, 682);
            this.gridControl2.TabIndex = 4;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView2_PopupMenuShowing);
            // 
            // gridColumn12
            // 
            this.gridColumn12.FieldName = "Id";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            // 
            // gridColumn13
            // 
            this.gridColumn13.FieldName = "Username";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            // 
            // gridColumn14
            // 
            this.gridColumn14.FieldName = "Password";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            // 
            // gridColumn15
            // 
            this.gridColumn15.FieldName = "HWID";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 3;
            // 
            // gridColumn16
            // 
            this.gridColumn16.FieldName = "IP";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 4;
            // 
            // gridColumn17
            // 
            this.gridColumn17.FieldName = "DiscordID";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 5;
            // 
            // gridColumn18
            // 
            this.gridColumn18.FieldName = "DiscordName";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 6;
            // 
            // gridColumn19
            // 
            this.gridColumn19.FieldName = "Skypes";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 7;
            // 
            // gridColumn20
            // 
            this.gridColumn20.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn20.FieldName = "LastRequest";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            // 
            // gridColumn21
            // 
            this.gridColumn21.FieldName = "Registered";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 9;
            // 
            // flatComboBox1
            // 
            this.flatComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.flatComboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.flatComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flatComboBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.flatComboBox1.ForeColor = System.Drawing.Color.White;
            this.flatComboBox1.FormattingEnabled = true;
            this.flatComboBox1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatComboBox1.ItemHeight = 18;
            this.flatComboBox1.Items.AddRange(new object[] {
            "LicenseKeys",
            "Licensing",
            "Logins",
            "Newsfeed",
            "Products",
            "Tokens"});
            this.flatComboBox1.Location = new System.Drawing.Point(359, 71);
            this.flatComboBox1.Name = "flatComboBox1";
            this.flatComboBox1.Size = new System.Drawing.Size(517, 24);
            this.flatComboBox1.TabIndex = 5;
            this.flatComboBox1.SelectionChangeCommitted += new System.EventHandler(this.flatComboBox1_SelectionChangeCommitted);
            // 
            // gridView2Strip
            // 
            this.gridView2Strip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.gridView2Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assoicatedAccountsToolStripMenuItem,
            this.ownedLicensesToolStripMenuItem,
            this.tokensToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.gridView2Strip.Name = "contextMenuStrip1";
            this.gridView2Strip.Size = new System.Drawing.Size(185, 92);
            this.gridView2Strip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.gridView2Strip_ItemClicked);
            // 
            // assoicatedAccountsToolStripMenuItem
            // 
            this.assoicatedAccountsToolStripMenuItem.Name = "assoicatedAccountsToolStripMenuItem";
            this.assoicatedAccountsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.assoicatedAccountsToolStripMenuItem.Text = "Assoicated Accounts";
            // 
            // ownedLicensesToolStripMenuItem
            // 
            this.ownedLicensesToolStripMenuItem.Name = "ownedLicensesToolStripMenuItem";
            this.ownedLicensesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ownedLicensesToolStripMenuItem.Text = "Owned Licenses";
            // 
            // tokensToolStripMenuItem
            // 
            this.tokensToolStripMenuItem.Name = "tokensToolStripMenuItem";
            this.tokensToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.tokensToolStripMenuItem.Text = "Tokens";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // gridView1Strip
            // 
            this.gridView1Strip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.gridView1Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.gridView1Strip.Name = "contextMenuStrip1";
            this.gridView1Strip.Size = new System.Drawing.Size(185, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "Assoicated Accounts";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem2.Text = "Owned Licenses";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem3.Text = "Tokens";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem4.Text = "Refresh";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 836);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Server_Load);
            this.formSkin1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.gridView2Strip.ResumeLayout(false);
            this.gridView1Strip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private theme.FormSkin formSkin1;
        private DevExpress.XtraGrid.Columns.GridColumn ip;
        private DevExpress.XtraGrid.Columns.GridColumn username;
        private DevExpress.XtraGrid.Columns.GridColumn authtoken;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colUsername;
        private DevExpress.XtraGrid.Columns.GridColumn colPassword;
        private DevExpress.XtraGrid.Columns.GridColumn colHWID;
        private DevExpress.XtraGrid.Columns.GridColumn colIP;
        private DevExpress.XtraGrid.Columns.GridColumn colLastRequest;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistered;
        private System.Windows.Forms.ContextMenuStrip gridView2Strip;
        private System.Windows.Forms.ToolStripMenuItem assoicatedAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ownedLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tokensToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscordId;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscordName;
        private DevExpress.XtraGrid.Columns.GridColumn colSkypes;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip gridView1Strip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private theme.FlatComboBox flatComboBox1;
        private MaverickClient.Theme.FlatClose flatClose1;
    }
}

