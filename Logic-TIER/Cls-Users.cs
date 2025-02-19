using DATA_TIER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Logic_TIER.ClsPeople;

namespace Logic_TIER
{
    public class Cls_Users
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int USerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int PersonID { get; set; }   

        public bool IsActive { get; set; }

        public ClsPeople PersonInfo { get; set; }


        public Cls_Users()
        {
            USerId = 0;
            UserName = "";
            Password = "";
            PersonID = 0;
            IsActive = false;
            Mode = enMode.AddNew;
        }

        public Cls_Users(int _USerId, string _UserName, string _Password, int _PersonID, bool _IsActive)
        {
            this.USerId = _USerId;
            this.UserName = _UserName;
            this.Password = _Password;
            this.PersonID = _PersonID;
            this.IsActive = _IsActive;
            this.PersonInfo = ClsPeople.GetPersonByID(_PersonID);
            Mode = enMode.Update;
        }

        private bool _Update()
        {
            return Sql_Users.Update_USER(this.USerId,this.PersonID,this.UserName,this.Password,this.IsActive);
        }
        private bool ADD()
        {
            this.USerId = Sql_Users.ADD_USER(this.PersonID, this.UserName, this.Password, this.IsActive);

            return this.USerId > 0;
        }

        public static Cls_Users GetUserByUserID(int UserID)
        {

            int PersonId = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;


            bool IsFound = Sql_Users.GetUserById(UserID, ref PersonId, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new Cls_Users(UserID, UserName, Password, PersonId, IsActive);
            else
                return null;
        }

        public static Cls_Users GetUserByPersonID(int PersonID)
        {

            int UserId = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;


            bool IsFound = Sql_Users.GetUserByPersonId(PersonID,ref UserId, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new Cls_Users(UserId, UserName, Password, PersonID, IsActive);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (ADD())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return Sql_Users.Delete(UserID);
        }

        public static bool IsExistByUserName(string UserName)
        {
            return Sql_Users.IsExistUserName(UserName);
        }
        public static bool IsExistByPersonID(int PersonID)
        {
            return Sql_Users.IsExistByPersonId(PersonID);
        }

        public static bool IsExistByUsernameAndPassword(string UserName, string Password)
        {
            return Sql_Users.IsExistByUsernameAndPassword(UserName, Password);
        }

        public static DataTable GetAllUsers()
        {
            return Sql_Users.GetallUsers();
           }
    }
}
