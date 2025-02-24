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
    public partial class FRM_ListTesTypes : Form
    {
        private DataTable dt = Cls_TestTypes.Getalltable();
        public FRM_ListTesTypes()
        {
            InitializeComponent();
        }

        private void _refresh()
        {
            dgvTestTypes.DataSource = dt;

        }
        private void FRM_ListTesTypes_Load(object sender, EventArgs e)
        {
            _refresh();
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 120;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Frm_UpdateTestTypescs frm = new Frm_UpdateTestTypescs(Convert.ToInt32((int)dgvTestTypes.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            _refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
