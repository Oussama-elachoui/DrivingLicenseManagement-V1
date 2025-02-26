using DrivingLicenseManagement_V1.Application.FRM_APP;
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

namespace DrivingLicenseManagement_V1.Application.LocalDrivingLisence
{
    public partial class Frm_LDLLISTE : Form
    {

        private DataTable dt = Cls_LocaldrivngLisence.Getall();

        private void _refresh()
        {        
         DataTable dt = Cls_LocaldrivngLisence.Getall();

        dgvLocalDrivingLicenseApplications.DataSource = dt;

        }
        public Frm_LDLLISTE()
        {
            InitializeComponent();
        }

        private void Frm_LDLLISTE_Load(object sender, EventArgs e)
        {
            dgvLocalDrivingLicenseApplications.DataSource = dt;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LOCALDRIVING frm_AppInfo = new LOCALDRIVING((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm_AppInfo.ShowDialog();
            _refresh();

        }

        private void eDITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ADDUPDATE_LDL frm_ADDUPDATE_LDL = new Frm_ADDUPDATE_LDL((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            frm_ADDUPDATE_LDL.ShowDialog();
            _refresh();

        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iNFIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            if(Cls_LocaldrivngLisence.StaticCancelled((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value))
                {
                MessageBox.Show("Application Cancelled Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _refresh();
            }
            else
            {
                MessageBox.Show("Could not Cancel applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dELETEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            Cls_LocaldrivngLisence LocalDrivingLicenseApplication =
                Cls_LocaldrivngLisence.Find(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.DeleteAll())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _refresh();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void aDDToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_ADDUPDATE_LDL lc = new Frm_ADDUPDATE_LDL();
            lc.ShowDialog();
            _refresh();

        }

        private void eDITToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Frm_ADDUPDATE_LDL frm_ADDUPDATE_LDL = new Frm_ADDUPDATE_LDL((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm_ADDUPDATE_LDL.ShowDialog();
            _refresh();

        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Frm_ADDUPDATE_LDL lc = new Frm_ADDUPDATE_LDL();
            lc.ShowDialog();
            _refresh();
        }
    }
}
