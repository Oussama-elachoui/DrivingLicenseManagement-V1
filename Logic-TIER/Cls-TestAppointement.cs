using DATA_TIER;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class Cls_TestAppointement
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public Cls_APPLICATION RetakeTestAppInfo { set; get; }
        public Cls_TestAppointement()

        {
            this.TestAppointmentID = -1;
            this.TestTypeID = 1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

        }

        public Cls_TestAppointement(int TestAppointmentID, int TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)

        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = Cls_APPLICATION.FindByid(RetakeTestApplicationID);
            Mode = enMode.Update;
        }
        private bool _AddNewTestAppointment()
        {

            this.TestAppointmentID = SQL_TestAppointement.AddTestAppointement(this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {

            return SQL_TestAppointement.Update(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

        public static Cls_TestAppointement Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (SQL_TestAppointement.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new Cls_TestAppointement(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        public static int GetTestID(int TestAppointmentID)
        {
            return SQL_TestAppointement.GetTestID(TestAppointmentID);
        }

        public int GetTestID()
        {
            return GetTestID(this.TestAppointmentID);
        }

        public  DataTable GetApplicationTestAppointments(int TestTypeID)
        {
            return GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);

        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return SQL_TestAppointement.GetdatatableByLocalIdAndTypeTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public bool IfHaveTestLocked(int TestTypeID)
        {
            return IfHaveTestLockedStatic(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static bool IfHaveTestLockedStatic(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return !SQL_TestAppointement.IfHaveTestLocked(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool IfHaveSuccedOnThatTest(int TestTypeID)
        {
            return IfHaveSuccedOnThatTest(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public static bool IfHaveSuccedOnThatTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return SQL_TestAppointement.IfHaveSuccedOnThatTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }
    }
}
