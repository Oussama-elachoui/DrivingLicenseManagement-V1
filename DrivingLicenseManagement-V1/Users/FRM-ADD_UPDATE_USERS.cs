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
    public partial class FRM_ADD_UPDATE_USERS : Form
    {
        private int _userId = -1;
        private Cls_Users _UserInfo;

        private enum FormMode
        {
            Add,
            Update
        }

        private FormMode _formMode = FormMode.Add;

        public FRM_ADD_UPDATE_USERS()
        {
            InitializeComponent();
            _formMode = FormMode.Add;
        }
        public FRM_ADD_UPDATE_USERS(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _formMode = FormMode.Update;
        }

        private void InitializeFill()
        {
            if(_formMode == FormMode.Add) 
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                chkIsActive.Enabled = false;
                lblUserID.Text = "Not Yet";
                _UserInfo = new Cls_Users();
            }
            else
            {
                ctrl_InfoPeersonByfilter1.FilterEnabled = false;

                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                chkIsActive.Enabled = true;
                lblUserID.Text = "Not Yet";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";

            }
        }


        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {

            if (_formMode == FormMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }
            if (ctrl_InfoPeersonByfilter1.PersonID != -1)
            {
                if(Cls_Users.IsExistByPersonID(ctrl_InfoPeersonByfilter1.PersonID))
                {
                    MessageBox.Show("This person is already a user");
                    return;
                }

                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                chkIsActive.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                btnSave.Enabled = true;


            }
            else
            {
                MessageBox.Show("Please select a person");
                return;
            }

        }

        private void _LoadData()
        {
            _UserInfo = Cls_Users.GetUserByUserID(_userId);
            if (_UserInfo == null)
            {

                MessageBox.Show("User not found");
                return;
            }

            ctrl_InfoPeersonByfilter1.FillInfo(_UserInfo.PersonID);
            txtUserName.Text = _UserInfo.UserName;
            txtPassword.Text = _UserInfo.Password;
            txtConfirmPassword.Text = _UserInfo.Password;
            chkIsActive.Checked = _UserInfo.IsActive;
        }
        private void FRM_ADD_UPDATE_USERS_Load(object sender, EventArgs e)
        {
            InitializeFill();
            if(_formMode==FormMode.Update)
            {
                _LoadData();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _UserInfo.PersonID = ctrl_InfoPeersonByfilter1.PersonID;
            _UserInfo.UserName = txtUserName.Text.Trim();
            _UserInfo.Password = txtPassword.Text.Trim();
            _UserInfo.IsActive = chkIsActive.Checked;

            if(_UserInfo.Save())
            {
                MessageBox.Show("User saved successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                lblUserID.Text = _UserInfo.USerId.ToString();
            }
            else
            {
                MessageBox.Show("Error while saving user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (_formMode!= FormMode.Update)
            {
                if (Cls_Users.IsExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "User Name already exists!");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            
        }
    }
}
