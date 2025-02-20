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
    public partial class ListApplicationTypes : Form
    {
        private DataTable Datatable = Cls_Application_Types.GetAll();

        private void Refresh()
        {
            dgvApplicationTypes.DataSource = Datatable;
        }
        public ListApplicationTypes()
        {
            InitializeComponent();
        }

        private void ListApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationTypes.DataSource = Datatable;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 110;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update frm = new Update((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
