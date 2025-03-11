using DATA_TIER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class CLS_Driver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public ClsPeople PersonInfo;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }
        public CLS_Driver()

        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }
        public CLS_Driver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = ClsPeople.GetPersonByID(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            

            this.DriverID = SQL_DRIVERS.ADD(PersonID, CreatedByUserID,DateTime.Now);


            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {

            return SQL_DRIVERS.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static CLS_Driver FindByDriverID(int DriverID)
        {

            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (SQL_DRIVERS.getDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))

                return new CLS_Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static CLS_Driver FindByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (SQL_DRIVERS.getDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))

                return new CLS_Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static DataTable GetAllDrivers()
        {
            return SQL_DRIVERS.GetALL();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }

        //public static DataTable GetLicenses(int DriverID)
        //{
        //    return clsLicense.GetDriverLicenses(DriverID);
        //}

        //public static DataTable GetInternationalLicenses(int DriverID)
        //{
        //    return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        //}

    }
}
