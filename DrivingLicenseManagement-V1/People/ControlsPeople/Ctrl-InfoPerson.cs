using DrivingLicenseManagement_V1.Properties;
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
using System.IO;

namespace DrivingLicenseManagement_V1.People.ControlsPeople
{
    public partial class Ctrl_InfoPerson : UserControl
    {
        private int _PersonID;
        private ClsPeople _Person;
        public int PersonID
        {
            get { return _PersonID; }
        }

        public ClsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }

        public Ctrl_InfoPerson()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Ctrl_InfoPerson_Load(object sender, EventArgs e)
        {

        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            if (_Person.Gendor == 1)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";

            }

            lblCountry.Text = _Person.NationalityCountryID.ToString();

            _LoadPersonImage();

        }
        public void FillInfo(int PersonID)
        {
            _Person = ClsPeople.GetPersonByID(PersonID);
            _PersonID = _Person.PersonID;


            if (_Person == null)
            {
                MessageBox.Show("No person found with the given PersonID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        public void FillInfo(string NationalNo)
        {
            _Person = ClsPeople.GetPersonByNatinalityNo(NationalNo);
            _PersonID = _Person.PersonID;


            if (_Person == null)
            {
                MessageBox.Show("No person found with the given PersonID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

    }
}
