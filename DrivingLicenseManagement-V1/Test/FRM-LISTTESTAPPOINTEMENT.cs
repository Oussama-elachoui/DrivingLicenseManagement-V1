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
    public partial class FRM_LISTTESTAPPOINTEMENT : Form
    {

        private int TestTypeID = 1;

        private int _LocalDrivingLicenseApplicationID = 1;

        public FRM_LISTTESTAPPOINTEMENT(int testTypeID, int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            TestTypeID = testTypeID;
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void FrontTest()
        {
            switch (TestTypeID)
            {
                case 1:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    break;
                case 2:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    break;
                case 3:
                    lblTitle.Text = "Driving Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Schedule_Test_512;
                    break;
            }

        }

        private void FRM_LISTTESTAPPOINTEMENT_Load(object sender, EventArgs e)
        {
            FrontTest();
            localDrivingApplication1.FindById(_LocalDrivingLicenseApplicationID);
            dgvLicenseTestAppointments.DataSource = Logic_TIER.Cls_TestAppointement.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, TestTypeID);
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            Cls_LocaldrivngLisence localDrivingInfo = Cls_LocaldrivngLisence.Find(_LocalDrivingLicenseApplicationID);

            if (!Cls_TestAppointement.IfHaveTestLockedStatic(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                MessageBox.Show("You have a unlocked test appointment, you can't add a new one", "Locked Test Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Cls_TestAppointement.IfHaveSuccedOnThatTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                MessageBox.Show("You have already succeeded on that test, you can't add a new one", "Succeeded Test Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Frm_scheduleTest frm_ScheduleTest = new Frm_scheduleTest(_LocalDrivingLicenseApplicationID, TestTypeID);
            frm_ScheduleTest.ShowDialog();
            dgvLicenseTestAppointments.DataSource = Logic_TIER.Cls_TestAppointement.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, TestTypeID);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;


            Frm_scheduleTest frm = new Frm_scheduleTest(_LocalDrivingLicenseApplicationID, TestTypeID, TestAppointmentID);
            frm.ShowDialog();
            dgvLicenseTestAppointments.DataSource = Logic_TIER.Cls_TestAppointement.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, TestTypeID);

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_TakeATest frm = new FRM_TakeATest((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value, TestTypeID);
            frm.ShowDialog();
            dgvLicenseTestAppointments.DataSource = Logic_TIER.Cls_TestAppointement.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, TestTypeID);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Cls_TestAppointement cls_TestAppointement = Cls_TestAppointement.Find((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            bool isLocked = cls_TestAppointement.IsLocked;

            if (isLocked)
            {
                takeTestToolStripMenuItem.Enabled = false;
            }
            else
            {
                takeTestToolStripMenuItem.Enabled = true;
            }
        }
    }
}
