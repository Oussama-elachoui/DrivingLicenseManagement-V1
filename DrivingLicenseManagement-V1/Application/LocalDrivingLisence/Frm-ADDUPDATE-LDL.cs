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
    public partial class Frm_ADDUPDATE_LDL : Form
    {
        private enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        private enMode _Mode = enMode.AddNew;

        private int _LocalDrivingLicenseID = -1;
        private Cls_LocaldrivngLisence LocaldrivngLisenceInfo;
        public Frm_ADDUPDATE_LDL()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public Frm_ADDUPDATE_LDL(int LocalID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalID;
            _Mode = enMode.Update;
        }


        private void _FillLicenseClasses()
        {
            DataTable dt = CLS_LICENCECLASSES.GetAllLicenseClasses();

            if (dt.Rows.Count > 0)
            {
                cbLicenseClass.BeginUpdate();
                cbLicenseClass.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    cbLicenseClass.Items.Add(dr["ClassName"].ToString());
                }

                cbLicenseClass.EndUpdate();
            }
            cbLicenseClass.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillInitialiazation()
        {
            _FillLicenseClasses();

            if (this._Mode == enMode.AddNew)
            {
                lblLocalDrivingLicebseApplicationID.Text = "[???]";
                lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                cbLicenseClass.SelectedIndex = 0;
                lblCreatedByUser.Text = "1";
                cbLicenseClass.Enabled = false;
                btnSave.Enabled = false;
                LocaldrivngLisenceInfo = new Cls_LocaldrivngLisence();
                lblFees.Text = Cls_Application_Types.FindByID(1).ApplicationFees.ToString();
            }
            else
            {
                lblTitle.Text = "Update License Class";
                ctrl_InfoPeersonByfilter1.FilterEnabled = false;

            }
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void _FillData()
        {
            LocaldrivngLisenceInfo = Cls_LocaldrivngLisence.Find(_LocalDrivingLicenseID);

            if (LocaldrivngLisenceInfo == null)
            {

                MessageBox.Show("No Record Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                ctrl_InfoPeersonByfilter1.FillInfo(LocaldrivngLisenceInfo.PersonID);
                lblLocalDrivingLicebseApplicationID.Text = LocaldrivngLisenceInfo.LOCALDRIVINGLISENCEID.ToString();
                lblApplicationDate.Text = LocaldrivngLisenceInfo.ApplicationDate.ToString("dd/MM/yyyy");
                cbLicenseClass.SelectedIndex = cbLicenseClass.Items.IndexOf(LocaldrivngLisenceInfo.LICENCECLASSESInfo.ClassName);
                lblFees.Text = LocaldrivngLisenceInfo.PaidFees.ToString();
                lblCreatedByUser.Text = LocaldrivngLisenceInfo.CreatedByUserID.ToString();

            }
        }
        private void Frm_ADDUPDATE_LDL_Load(object sender, EventArgs e)
        {
            _FillInitialiazation();
            if (this._Mode == enMode.Update)
            {
                _FillData();
            }
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                cbLicenseClass.Enabled = true;
                Info.SelectedTab = Info.TabPages["tpInfo"];
                return;
            }
            if (ctrl_InfoPeersonByfilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Info.SelectedTab = Info.TabPages["tpInfo"];
            cbLicenseClass.Enabled = true;
            btnSave.Enabled = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Cls_LocaldrivngLisence.CheckIfPersonHasDemandeLocalDrivingLicenseBefore_Static(ctrl_InfoPeersonByfilter1.PersonID, CLS_LICENCECLASSES.Find(cbLicenseClass.SelectedItem.ToString()).LicenseClassID))
            {
                MessageBox.Show("This person has already applied for this license class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LocaldrivngLisenceInfo.LicenseClassID = CLS_LICENCECLASSES.Find(cbLicenseClass.Text).LicenseClassID;
            LocaldrivngLisenceInfo.PersonID = ctrl_InfoPeersonByfilter1.PersonID;
            LocaldrivngLisenceInfo.ApplicationDate = DateTime.Parse(lblApplicationDate.Text);
            LocaldrivngLisenceInfo.ApplicationTypeID = 1;
            LocaldrivngLisenceInfo.ApplicationStatus = 1;
            LocaldrivngLisenceInfo.LastStatusDate = DateTime.Now;
            LocaldrivngLisenceInfo.PaidFees = decimal.Parse(lblFees.Text);
            LocaldrivngLisenceInfo.CreatedByUserID = 1;




            if (LocaldrivngLisenceInfo.Save())
            {

                MessageBox.Show("Record Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error in saving", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
