using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement_V1.Application
{
    public partial class LOCALDRIVING : Form
    {
        public LOCALDRIVING(int id)
        {
            InitializeComponent();
            localDrivingApplication1.FindById(id);
        }

        private void LOCALDRIVING_Load(object sender, EventArgs e)
        {

        }
    }
}
