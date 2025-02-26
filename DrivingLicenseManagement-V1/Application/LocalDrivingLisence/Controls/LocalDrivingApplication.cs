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

namespace DrivingLicenseManagement_V1.Application.LocalDrivingLisence.Controls
{
    public partial class LocalDrivingApplication : UserControl
    {
        private int _idLocaldrivingID = -1;
        private Cls_LocaldrivngLisence _LocaldrivngLisenceInfo;
        public int IdLocaldrivingID
        {
            get { return _idLocaldrivingID; }
        }
        public Cls_LocaldrivngLisence LocaldrivngLisenceInfo1
        {
            get { return _LocaldrivngLisenceInfo; }
        }
        public LocalDrivingApplication()
        {
            InitializeComponent();
        }

        public void FindById(int ID)
        {
            _LocaldrivngLisenceInfo = Cls_LocaldrivngLisence.Find(ID);
            if (_LocaldrivngLisenceInfo == null)
            {
                MessageBox.Show("No Record Found");
                return;

            }
            _idLocaldrivingID = ID;

            lblLocalDrivingLicenseApplicationID.Text = _LocaldrivngLisenceInfo.LOCALDRIVINGLISENCEID.ToString();
            lblAppliedFor.Text = _LocaldrivngLisenceInfo.LICENCECLASSESInfo.ClassName;
            lblPassedTests.Text = _LocaldrivngLisenceInfo.NumberTestLocked().ToString() + "/3";

            applicationInfo1.FindById(_LocaldrivngLisenceInfo.APPLICATIONID);

        }
        private void LocalDrivingApplication_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
