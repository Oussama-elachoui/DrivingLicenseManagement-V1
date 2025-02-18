namespace DrivingLicenseManagement_V1.People
{
    partial class FRM_InfoPersonByfilter
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
            this.ctrl_InfoPeersonByfilter1 = new DrivingLicenseManagement_V1.People.ControlsPeople.Ctrl_InfoPeersonByfilter();
            this.SuspendLayout();
            // 
            // ctrl_InfoPeersonByfilter1
            // 
            this.ctrl_InfoPeersonByfilter1.Location = new System.Drawing.Point(47, 63);
            this.ctrl_InfoPeersonByfilter1.Name = "ctrl_InfoPeersonByfilter1";
            this.ctrl_InfoPeersonByfilter1.Size = new System.Drawing.Size(837, 378);
            this.ctrl_InfoPeersonByfilter1.TabIndex = 0;
            // 
            // FRM_InfoPersonByfilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 529);
            this.Controls.Add(this.ctrl_InfoPeersonByfilter1);
            this.Name = "FRM_InfoPersonByfilter";
            this.Text = "FRM_InfoPersonByfilter";
            this.ResumeLayout(false);

        }

        #endregion

        private ControlsPeople.Ctrl_InfoPeersonByfilter ctrl_InfoPeersonByfilter1;
    }
}