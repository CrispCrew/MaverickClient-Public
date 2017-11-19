namespace MaverickClient
{
    partial class ActivationPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivationPopup));
            this.formSkin1 = new theme.FormSkin();
            this.OkayButton = new theme.FlatButton();
            this.ActivationMessage = new theme.FlatLabel();
            this.flatClose1 = new MaverickClient.Theme.FlatClose();
            this.formSkin1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.OkayButton);
            this.formSkin1.Controls.Add(this.ActivationMessage);
            this.formSkin1.Controls.Add(this.flatClose1);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.formSkin1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.formSkin1.HeaderMaximize = false;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(367, 170);
            this.formSkin1.TabIndex = 1;
            this.formSkin1.Text = "Maverick Cheats :: Key Activation";
            // 
            // OkayButton
            // 
            this.OkayButton.BackColor = System.Drawing.Color.Transparent;
            this.OkayButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.OkayButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OkayButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.OkayButton.Location = new System.Drawing.Point(137, 134);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Rounded = false;
            this.OkayButton.Size = new System.Drawing.Size(92, 29);
            this.OkayButton.TabIndex = 131;
            this.OkayButton.Text = "Okay";
            this.OkayButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
            // 
            // ActivationMessage
            // 
            this.ActivationMessage.BackColor = System.Drawing.Color.Transparent;
            this.ActivationMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActivationMessage.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.ActivationMessage.ForeColor = System.Drawing.Color.White;
            this.ActivationMessage.Location = new System.Drawing.Point(0, 0);
            this.ActivationMessage.Name = "ActivationMessage";
            this.ActivationMessage.Size = new System.Drawing.Size(367, 170);
            this.ActivationMessage.TabIndex = 130;
            this.ActivationMessage.Text = "{Msg}";
            this.ActivationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flatClose1
            // 
            this.flatClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatClose1.BackColor = System.Drawing.Color.White;
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatClose1.Font = new System.Drawing.Font("Marlett", 10F);
            this.flatClose1.Location = new System.Drawing.Point(337, 12);
            this.flatClose1.Name = "flatClose1";
            this.flatClose1.Size = new System.Drawing.Size(18, 18);
            this.flatClose1.TabIndex = 9;
            this.flatClose1.Text = "flatClose1";
            this.flatClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // ActivationPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 170);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivationPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActivationPopup";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.ActivationPopup_Load);
            this.formSkin1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private theme.FormSkin formSkin1;
        private MaverickClient.Theme.FlatClose flatClose1;
        private theme.FlatLabel ActivationMessage;
        private theme.FlatButton OkayButton;
    }
}