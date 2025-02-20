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

namespace DrivingLicenseManagement_V1.Test
{
    public partial class Frm_UpdateTestTypescs : Form
    {
        private int _Id = -1;
        private Cls_TestTypes Cls_TestTypes;
        public Frm_UpdateTestTypescs(int Id)
        {
            InitializeComponent();
            this._Id = Id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_UpdateTestTypescs_Load(object sender, EventArgs e)
        {
            Cls_TestTypes = Cls_TestTypes.Find(_Id);
            if (Cls_TestTypes == null)
            {
                MessageBox.Show("Test Type not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtTitle.Text = Cls_TestTypes.TestTypeTitle;
            txtDescription.Text = Cls_TestTypes.TestTypeDescription;
            txtFees.Text = Cls_TestTypes.TestTypeFees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Cls_TestTypes.TestTypeTitle = txtTitle.Text;
            Cls_TestTypes.TestTypeDescription = txtDescription.Text;
            Cls_TestTypes.TestTypeFees = float.Parse(txtFees.Text);

            if (Cls_TestTypes.Update())
            {
                MessageBox.Show("Test Type Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Updating Test Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
