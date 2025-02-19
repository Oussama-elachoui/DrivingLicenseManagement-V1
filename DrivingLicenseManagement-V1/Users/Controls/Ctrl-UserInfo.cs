using Logic_TIER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement_V1.Users.Controls
{
    public partial class Ctrl_UserInfo : UserControl
    {

        private int _userId=-1;
        private Cls_Users _UserInfo;

        public int UserId
        {
            get { return _userId; }
        }

        public Cls_Users SelectedUserInfo
        {
            get { return _UserInfo; }
        }
        public Ctrl_UserInfo()
        {
            InitializeComponent();
        }

        public void FiLLByUserId(int userId)
        {
            _UserInfo = Cls_Users.GetUserByUserID(userId);
            if(_UserInfo == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            _userId = userId;
            ctrl_InfoPerson1.FillInfo(_UserInfo.PersonID);

            lblUserID.Text = _UserInfo.USerId.ToString();
            lblUserName.Text = _UserInfo.UserName;

            if(_UserInfo.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }
        private void Ctrl_UserInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
