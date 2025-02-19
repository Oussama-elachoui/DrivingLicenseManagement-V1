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
    public partial class Frm_UserInfo : Form
    {
        private int _userId = -1;
        public Frm_UserInfo(int UserID)
        {
            InitializeComponent();
            _userId = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_UserInfo_Load(object sender, EventArgs e)
        {
            ctrl_UserInfo1.FiLLByUserId(_userId);
        }
    }
}
