namespace DrivingLicenseManagement_V1.Application.FRM_APP
{
    partial class frm_appInfo
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
            this.applicationInfo1 = new DrivingLicenseManagement_V1.Application.Controls.ApplicationInfo();
            this.SuspendLayout();
            // 
            // applicationInfo1
            // 
            this.applicationInfo1.Location = new System.Drawing.Point(3, 12);
            this.applicationInfo1.Name = "applicationInfo1";
            this.applicationInfo1.Size = new System.Drawing.Size(888, 212);
            this.applicationInfo1.TabIndex = 0;
            // 
            // frm_appInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 232);
            this.Controls.Add(this.applicationInfo1);
            this.Name = "frm_appInfo";
            this.Text = "frm_appInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ApplicationInfo applicationInfo1;
    }
}