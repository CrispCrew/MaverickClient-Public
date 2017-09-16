namespace Updater
{
    partial class Download
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
            this.formSkin1 = new theme.FormSkin();
            this.flatLabel2 = new theme.FlatLabel();
            this.flatLabel1 = new theme.FlatLabel();
            this.flatProgressBar1 = new theme.FlatProgressBar();
            this.formSkin1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.flatLabel2);
            this.formSkin1.Controls.Add(this.flatLabel1);
            this.formSkin1.Controls.Add(this.flatProgressBar1);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.formSkin1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.formSkin1.HeaderMaximize = false;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(795, 244);
            this.formSkin1.TabIndex = 0;
            this.formSkin1.Text = "Maverick Cheats :: Download Manager";
            // 
            // flatLabel2
            // 
            this.flatLabel2.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.flatLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatLabel2.Location = new System.Drawing.Point(175, 196);
            this.flatLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(452, 37);
            this.flatLabel2.TabIndex = 131;
            this.flatLabel2.Text = "0/0";
            this.flatLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flatLabel1
            // 
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.flatLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatLabel1.Location = new System.Drawing.Point(175, 160);
            this.flatLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(452, 37);
            this.flatLabel1.TabIndex = 130;
            this.flatLabel1.Text = "Current Task: ";
            this.flatLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flatProgressBar1
            // 
            this.flatProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.flatProgressBar1.DarkerProgress = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(148)))), ((int)(((byte)(92)))));
            this.flatProgressBar1.Location = new System.Drawing.Point(179, 105);
            this.flatProgressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flatProgressBar1.Maximum = 100;
            this.flatProgressBar1.Name = "flatProgressBar1";
            this.flatProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatProgressBar1.Size = new System.Drawing.Size(448, 42);
            this.flatProgressBar1.TabIndex = 0;
            this.flatProgressBar1.Text = "flatProgressBar1";
            this.flatProgressBar1.Value = 25;
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 244);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Download";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Download_Load);
            this.formSkin1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private theme.FormSkin formSkin1;
        private theme.FlatProgressBar flatProgressBar1;
        private theme.FlatLabel flatLabel2;
        private theme.FlatLabel flatLabel1;
    }
}