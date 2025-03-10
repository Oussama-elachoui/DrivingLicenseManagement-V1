using DrivingLicenseManagement_V1.Properties;
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

namespace DrivingLicenseManagement_V1.Test.controls
{
    public partial class Testapt : UserControl
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;



        private int _TestTypeID = 1;
        private Cls_LocaldrivngLisence _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private Cls_TestAppointement _TestAppointment;
        private int _TestAppointmentID = -1;

        public int TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case 1:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case 2:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case 3:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public Testapt()
        {
            InitializeComponent();
        }
        public void _Load(int LocalDrivingLicenseApplicationID, int AppointmentID=-1)
        {

            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = Cls_LocaldrivngLisence.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicenseApplication.DoesAttendBytest(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;



            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = Cls_Application_Types.FindByID(7).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }


            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LOCALDRIVINGLISENCEID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LICENCECLASSESInfo.ClassName;
            lblFullName.Text = ClsPeople.GetPersonByID(_LocalDrivingLicenseApplication.PersonID).FullName;


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = Cls_TestTypes.Find(_TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new Cls_TestAppointement();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;

            }


            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;
          

        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && !Cls_TestAppointement.IfHaveTestLockedStatic(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }

            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {

            if (_TestAppointment.IsLocked)
            {
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else

            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = Cls_TestAppointement.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }
        private bool _HandleRetakeApplication()
        {

            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {

                Cls_APPLICATION Application = new Cls_APPLICATION();

                Application.PersonID = _LocalDrivingLicenseApplication.PersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = 7;
                Application.ApplicationStatus = 2;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = (decimal)Cls_Application_Types.FindByID(7).ApplicationFees;
                Application.CreatedByUserID = 1;

                if (!Application.SaveAPPLICATION())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.APPLICATIONID;

            }
            return true;
        }


        private void Testapt_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LOCALDRIVINGLISENCEID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = decimal.Parse(lblFees.Text);
            _TestAppointment.CreatedByUserID = 1;


            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
