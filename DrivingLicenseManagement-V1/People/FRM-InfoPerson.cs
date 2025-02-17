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

namespace DrivingLicenseManagement_V1.People
{
    public partial class FRM_InfoPerson : Form
    {
        private int _PersonID;
        private ClsPeople _Person;

        public FRM_InfoPerson()
        {
            InitializeComponent();
        }
        public FRM_InfoPerson(int PersonId)
        {
            InitializeComponent();
            _PersonID = PersonId;
        }

        private void FRM_InfoPerson_Load(object sender, EventArgs e)
        {
            ctrl_InfoPerson1.FillInfo(_PersonID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
