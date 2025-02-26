using DrivingLicenseManagement_V1.People;
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

namespace DrivingLicenseManagement_V1.Application.Controls
{
    public partial class ApplicationInfo : UserControl
    {
        private int _applicationId =-1;
        private Cls_APPLICATION _Applicationinfo;

        public int ApplicationID
        {
            get { return _applicationId; }
        }

        public Cls_APPLICATION Applicationinfo
        {
            get { return _Applicationinfo; }
        }


        public ApplicationInfo()
        {
            InitializeComponent();
        }

        private void ApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        public void FindById(int applicationId)
        {
            _Applicationinfo = Cls_APPLICATION.FindByid(applicationId);

            if (_Applicationinfo == null)
            {
                MessageBox.Show("Application not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _applicationId = applicationId;

            lblApplicationID.Text = _Applicationinfo.APPLICATIONID.ToString();
            lblStatus.Text = _Applicationinfo.StatusText;
            lblFees.Text = _Applicationinfo.PaidFees.ToString();
            lblType.Text = Cls_Application_Types.FindByID(_Applicationinfo.ApplicationTypeID).ApplicationTypeTitle;
            lblApplicant.Text = ClsPeople.GetPersonByID(_Applicationinfo.PersonID).FullName;
            lblDate.Text = _Applicationinfo.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _Applicationinfo.LastStatusDate.ToShortDateString();
            lblCreatedByUser.Text = Cls_Users.GetUserByUserID(_Applicationinfo.CreatedByUserID).UserName;


        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FRM_InfoPerson fRM_InfoPerson = new FRM_InfoPerson(_Applicationinfo.PersonID);
            fRM_InfoPerson.ShowDialog();

        }
    }
}
