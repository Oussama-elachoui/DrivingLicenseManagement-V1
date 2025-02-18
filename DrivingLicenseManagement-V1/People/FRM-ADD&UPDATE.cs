using DrivingLicenseManagement_V1.Properties;
using Logic_TIER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Lifetime;
using static Logic_TIER.ClsPeople;



namespace DrivingLicenseManagement_V1.People
{

    public partial class FRM_ADD_UPDATE : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        private enum enGendor { Male=1,Female=2 }
        private enGendor _enGendor;
        private int _PersonID = -1;
        private ClsPeople _Person;

        private enum FormMode
        {
            Add,
            Update
        }
        private FormMode _Mode= FormMode.Add;
        public FRM_ADD_UPDATE()
        {
            InitializeComponent();
            _Mode = FormMode.Add;
        }
        public FRM_ADD_UPDATE(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
            _Mode = FormMode.Update;
        }

        private void FillCountries()
        {
            DataTable dataTable = Cls_Countries.GetAllcountries();

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return;
            }

            cbCountry.Items.Clear(); 

            foreach (DataRow row in dataTable.Rows)
            {
                string countryName = row["CountryName"]?.ToString() ?? "Inconnu"; 
                cbCountry.Items.Add(countryName);
            }

        }
        private void Initialfill()
        {
            FillCountries();
            if (_Mode== FormMode.Add)
            {
                lblTitle.Text = "Add New Person";
                _Person = new ClsPeople();

            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtSecondName.Text = "";

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillData()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;

            txtLastName.Text = _Person.LastName;
            txtThirdName.Text = _Person.ThirdName;
            txtNationalNo.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;

            if (_Person.Gendor == 1)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            dtpDateOfBirth.Value = _Person.DateOfBirth;

            cbCountry.SelectedIndex = cbCountry.SelectedIndex = cbCountry.FindString(_Person.Cls_Countries.CountryName);

            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;

            }

            llRemoveImage.Visible = (_Person.ImagePath != "");
        
    }
        private void FRM_ADD_UPDATE_Load(object sender, EventArgs e)
        {
            Initialfill();

            if (_Mode == FormMode.Update)
            {
                _Person = ClsPeople.GetPersonByID(_PersonID);
                if (_Person == null)
                {
                    MessageBox.Show("Could not find this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                FillData();

            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;



            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }
        private bool _HandlePersonImage()
        {


            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;
            _Person.PersonID = int.Parse(lblPersonID.Text);
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female; _Person.NationalityCountryID = Cls_Countries.FindByCountryName(cbCountry.Text).CountryID;

            _Person.ImagePath = pbPersonImage.ImageLocation?.ToString() ?? "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = FormMode.Add;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void FRM_ADD_UPDATE_Validating(object sender, CancelEventArgs e)
        {
            

                TextBox Temp = ((TextBox)sender);
                if (string.IsNullOrEmpty(Temp.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(Temp, "This field is required!");
                }
                else
                {
                    //e.Cancel = false;
                    errorProvider1.SetError(Temp, null);
                }

            
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
            }
                else
                {
                    errorProvider1.SetError(txtNationalNo, null);
                }

            if (txtNationalNo.Text.Trim() != _Person.NationalNo && ClsPeople.IsExistNationalNo(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }


        }
    }

