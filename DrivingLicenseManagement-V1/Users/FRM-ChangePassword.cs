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

namespace DrivingLicenseManagement_V1.Users
{
    public partial class FRM_ChangePassword : Form
    {
        private int _userId = -1;
        private Cls_Users UserInfo;

        public FRM_ChangePassword(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void FRM_ChangePassword_Load(object sender, EventArgs e)
        {
            UserInfo = Cls_Users.GetUserByUserID(_userId);
            if (UserInfo == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            ctrl_UserInfo1.FiLLByUserId(_userId);
            

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (UserInfo.Password == txtCurrentPassword.Text)
            {
                errorProvider1.SetError(txtCurrentPassword, "");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "Invalid Password");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserInfo.Password = txtNewPassword.Text;

            if(UserInfo.Save())
            {
                MessageBox.Show("Password Changed Successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Changing Password");
            }
        }
    }
}
