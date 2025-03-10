namespace DrivingLicenseManagement_V1.Test
{
    partial class Frm_scheduleTest
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
            this.testapt1 = new DrivingLicenseManagement_V1.Test.controls.Testapt();
            this.SuspendLayout();
            // 
            // testapt1
            // 
            this.testapt1.Location = new System.Drawing.Point(12, 44);
            this.testapt1.Name = "testapt1";
            this.testapt1.Size = new System.Drawing.Size(582, 794);
            this.testapt1.TabIndex = 0;
            this.testapt1.TestTypeID = 1;
            // 
            // Frm_scheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 850);
            this.Controls.Add(this.testapt1);
            this.Name = "Frm_scheduleTest";
            this.Text = "Frm_scheduleTest";
            this.Load += new System.EventHandler(this.Frm_scheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private controls.Testapt testapt1;
    }
}