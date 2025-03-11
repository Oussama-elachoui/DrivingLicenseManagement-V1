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
using static System.Windows.Forms.DataGrid;

namespace DrivingLicenseManagement_V1.Test
{
    public partial class FRM_TakeATest : Form
    {
        private int _TestAppointmentID = -1;

        private Cls_TestAppointement TestAppointmentINFO;

        private int _TestTypeID = 1;

        private Cls_Test TestInfo;
        private int _TestID = -1;
        public FRM_TakeATest(int TestAPPointementID, int TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAPPointementID;
            _TestTypeID = TestTypeID;
        }

        private void PictTestType()
        {
            switch (_TestTypeID)
            {
                case 1:
                    lblTitle.Text = "Vision Test";
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    break;
                case 2:
                    lblTitle.Text = "Written Test";
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    break;
                case 3:
                    lblTitle.Text = "Driving Test";
                    pbTestTypeImage.Image = Properties.Resources.Schedule_Test_512;
                    break;
            }
        }

        private void _FillInfo()
        {
            PictTestType();
            TestAppointmentINFO = Cls_TestAppointement.Find(_TestAppointmentID);

            if (TestAppointmentINFO == null)
            {
                MessageBox.Show("Test Appointment Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = TestAppointmentINFO.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = Cls_LocaldrivngLisence.Find(TestAppointmentINFO.LocalDrivingLicenseApplicationID).LICENCECLASSESInfo.ClassName;
            lblFullName.Text = ClsPeople.GetPersonByID(Cls_LocaldrivngLisence.Find(TestAppointmentINFO.LocalDrivingLicenseApplicationID).PersonID).FullName;
            Date.Text = TestAppointmentINFO.AppointmentDate.ToShortDateString();
            lblFees.Text = TestAppointmentINFO.PaidFees.ToString();
        }
        private void FRM_TakeATest_Load(object sender, EventArgs e)
        {
            _FillInfo();

            _TestID = TestAppointmentINFO.GetTestID();

            if (_TestID != -1)
            {
                TestInfo = Cls_Test.Find(_TestID);
                if (TestInfo != null)
                {
                    txtNotes.Text = TestInfo.Notes;
                    if (TestInfo.TestResult)
                    {
                        rbPass.Checked = true;
                    }
                    else
                    {
                        rbFail.Checked = true;
                    }
                }
                btnSave.Enabled = true;

            }
            else
            {
                TestInfo = new Cls_Test();

            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TestInfo.TestAppointmentID = _TestAppointmentID;
            TestInfo.Notes = txtNotes.Text;
            TestInfo.CreatedByUserID = 1;
            TestInfo.TestResult = rbPass.Checked;


            if (TestInfo.Save())
            {
                MessageBox.Show("Test Saved Successfully", "Test Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Saving Test", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

