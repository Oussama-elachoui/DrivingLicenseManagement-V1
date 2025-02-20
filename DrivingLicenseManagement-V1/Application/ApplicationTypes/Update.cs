using Logic_TIER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement_V1.Application.ApplicationTypes
{
    public partial class Update : Form
    {
        


        private int _ApplicationTypeID = -1;
        private Cls_Application_Types ApplicationTypesInfo;
        public Update(int ID)
        {
            InitializeComponent();
            _ApplicationTypeID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeFill()
        {
            txtTitle.Text = "";
            txtFees.Text = "";
            lblApplicationTypeID.Text = "Not yet";
        }
        private void Update_Load(object sender, EventArgs e)
        {
            InitializeFill();
            ApplicationTypesInfo = Cls_Application_Types.FindByID(_ApplicationTypeID);
            if (ApplicationTypesInfo == null)
            {
                MessageBox.Show("Application Type not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                lblApplicationTypeID.Text = ApplicationTypesInfo.ApplicationTypeID.ToString();
                txtTitle.Text = ApplicationTypesInfo.ApplicationTypeTitle;
                txtFees.Text = ApplicationTypesInfo.ApplicationFees.ToString();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApplicationTypesInfo.ApplicationTypeTitle = txtTitle.Text;
            ApplicationTypesInfo.ApplicationFees = float.Parse(txtFees.Text);
 
            if (ApplicationTypesInfo.Update())
            {
                MessageBox.Show("Application Type Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error while updating Application Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
