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

namespace DrivingLicenseManagement_V1.People.ControlsPeople
{
    public partial class Ctrl_InfoPeersonByfilter : UserControl
    {

        public int PersonID
        {
            get { return ctrl_InfoPerson1.PersonID; }
        }
        public ClsPeople SelectedPersonInfo
        {
            get { return ctrl_InfoPerson1.SelectedPersonInfo; }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public Ctrl_InfoPeersonByfilter()
        {
            InitializeComponent();
        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex) 
                
            {
                case 0:
                    ctrl_InfoPerson1.FillInfo(txtFilterValue.Text);

                    break;
                case 1:
                    ctrl_InfoPerson1.FillInfo(Convert.ToInt32(txtFilterValue.Text));
                    break;
                default:
                    break;



            }
            
        }

        private void Ctrl_InfoPeersonByfilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Please enter a value");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, "");
            
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            FRM_ADD_UPDATE frm = new FRM_ADD_UPDATE();
            frm.DataBack += DatabackFunction;
            frm.ShowDialog();

        }

        private void DatabackFunction(object sender,int personId)
        {
            ctrl_InfoPerson1.FillInfo(personId);

        }
    }
}
