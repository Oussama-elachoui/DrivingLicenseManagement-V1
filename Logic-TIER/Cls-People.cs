using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class ClsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string NationalNo { set; get; }
        public DateTime DateOfBirth { set; get; }
        public short Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }

        private string _ImagePath;
        public Cls_Countries Cls_Countries;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public ClsPeople()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private ClsPeople(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)

        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.Cls_Countries= Cls_Countries.FindById(NationalityCountryID);
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {

            this.PersonID = DATA_TIER.SqlPeople.AddPerson(
                this.NationalNo,this.FirstName, this.SecondName, this.ThirdName,
                this.LastName,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {

            return DATA_TIER.SqlPeople.Update(
                this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);
        }


        public static ClsPeople GetPersonByID(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = DATA_TIER.SqlPeople.GetPersonByID
                                (
                                    PersonID, ref NationalNo, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName,  ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                return new ClsPeople(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }



        public static ClsPeople GetPersonByNatinalityNo(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "",  Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            int PersonID = -1;
            short Gendor = 0;

            bool IsFound = DATA_TIER.SqlPeople.GetPersonByNationalNo
                                (
                                    NationalNo, ref PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                return new ClsPeople(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }


        public static DataTable GetAllPeople()
        {
            return DATA_TIER.SqlPeople.GetAllPeople();
        }

        public static bool DeletePerson(int ID)
        {
            return DATA_TIER.SqlPeople.Delete(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return DATA_TIER.SqlPeople.IsExistPersonID(ID);
        }

        public static bool IsExistNationalNo(string NationlNo)
        {
            return DATA_TIER.SqlPeople.IsExistNationalNo(NationlNo);
        }
        public static bool IsExistEmail(string Email)
        {
            return DATA_TIER.SqlPeople.IsExistEmail(Email);
        }
        public static bool IsExistPhone(string Phone)
        {
            return DATA_TIER.SqlPeople.IsExistPhone(Phone);
        }





    }
}
