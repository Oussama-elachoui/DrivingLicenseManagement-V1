namespace DrivingLicenseManagement_V1.Users
{
    partial class Frm_UserInfo
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
            this.ctrl_UserInfo1 = new DrivingLicenseManagement_V1.Users.Controls.Ctrl_UserInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrl_UserInfo1
            // 
            this.ctrl_UserInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrl_UserInfo1.Name = "ctrl_UserInfo1";
            this.ctrl_UserInfo1.Size = new System.Drawing.Size(832, 415);
            this.ctrl_UserInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(707, 419);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Frm_UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 470);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrl_UserInfo1);
            this.Name = "Frm_UserInfo";
            this.Text = "Frm_UserInfo";
            this.Load += new System.EventHandler(this.Frm_UserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Ctrl_UserInfo ctrl_UserInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}