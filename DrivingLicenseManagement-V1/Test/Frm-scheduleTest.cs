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
    public partial class Frm_scheduleTest : Form
    {
        private int _TestTypeID = 1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;

        public Frm_scheduleTest(int localDrivingID,int Testtype,int testAPT=-1 )
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = localDrivingID;
            _TestTypeID = Testtype;
            _TestAppointmentID = testAPT;
        }

        public Frm_scheduleTest()
        {
            InitializeComponent();
        }

        private void Frm_scheduleTest_Load(object sender, EventArgs e)
        {
            testapt1._Load(_LocalDrivingLicenseApplicationID, _TestAppointmentID);
            testapt1.TestTypeID = _TestTypeID;

        }
    }
}
