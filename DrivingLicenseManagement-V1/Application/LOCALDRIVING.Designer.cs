namespace DrivingLicenseManagement_V1.Application
{
    partial class LOCALDRIVING
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
            this.localDrivingApplication1 = new DrivingLicenseManagement_V1.Application.LocalDrivingLisence.Controls.LocalDrivingApplication();
            this.SuspendLayout();
            // 
            // localDrivingApplication1
            // 
            this.localDrivingApplication1.Location = new System.Drawing.Point(12, 12);
            this.localDrivingApplication1.Name = "localDrivingApplication1";
            this.localDrivingApplication1.Size = new System.Drawing.Size(931, 361);
            this.localDrivingApplication1.TabIndex = 0;
            // 
            // LOCALDRIVING
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 399);
            this.Controls.Add(this.localDrivingApplication1);
            this.Name = "LOCALDRIVING";
            this.Text = "LOCALDRIVING";
            this.Load += new System.EventHandler(this.LOCALDRIVING_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LocalDrivingLisence.Controls.LocalDrivingApplication localDrivingApplication1;
    }
}