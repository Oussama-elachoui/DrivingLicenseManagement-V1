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

namespace DrivingLicenseManagement_V1.Users
{
    public partial class Frm_List : Form
    {
        private DataTable DataTable = Cls_Users.GetAllUsers();

        private void Refresh()
        {
            dgvUsers.DataSource = DataTable;
        }
        public Frm_List()
        {
            InitializeComponent();
        }

        private void Frm_List_Load(object sender, EventArgs e)
        {
            dgvUsers.DataSource = DataTable;
            cbFilterBy.SelectedIndex = 0;

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 110;

            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[1].Width = 120;

            dgvUsers.Columns[2].HeaderText = "FullName";
            dgvUsers.Columns[2].Width = 350;

            dgvUsers.Columns[3].HeaderText = "UserName";
            dgvUsers.Columns[3].Width = 120;

            dgvUsers.Columns[4].HeaderText = "IsActive";
            dgvUsers.Columns[4].Width = 120;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                DataTable.DefaultView.RowFilter = "";
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                DataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                DataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FRM_ADD_UPDATE_USERS FRM = new FRM_ADD_UPDATE_USERS();
            FRM.ShowDialog();
            Refresh();
        }

        private void eDITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_ADD_UPDATE_USERS FRM = new FRM_ADD_UPDATE_USERS((int)dgvUsers.CurrentRow.Cells[0].Value);
            FRM.ShowDialog();
            Refresh();
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (Cls_Users.DeleteUser(((int)dgvUsers.CurrentRow.Cells[0].Value))) ;
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Refresh();
                }
                
            }

            else
                MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void iNFOToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Frm_UserInfo frm_UserInfo = new Frm_UserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm_UserInfo.ShowDialog();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            FRM_ADD_UPDATE_USERS FRM = new FRM_ADD_UPDATE_USERS();
            FRM.ShowDialog();
            Refresh();
        }
    }
}
