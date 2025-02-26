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
    public class Cls_LocaldrivngLisence : Cls_APPLICATION
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LOCALDRIVINGLISENCEID { get; set; }
        public int LicenseClassID { get; set; }

        public CLS_LICENCECLASSES LICENCECLASSESInfo { get; set; }



        public Cls_LocaldrivngLisence(int APPLICATIONID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int LOCALDRIVINGLISENCEID, int ApplicationID, int LicenseClassID)
        {
            this.LOCALDRIVINGLISENCEID = LOCALDRIVINGLISENCEID;
            this.APPLICATIONID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
            this.LICENCECLASSESInfo = CLS_LICENCECLASSES.Find(LicenseClassID);
        }

        public Cls_LocaldrivngLisence()
        {

            this.LOCALDRIVINGLISENCEID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;

        }

        public static Cls_LocaldrivngLisence Find(int LOCALDRIVINGLISENCEID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            bool Isfound = SQL_LOCALDRIVINGLISENCE.FindByid(LOCALDRIVINGLISENCEID, ref ApplicationID, ref LicenseClassID);

            if (Isfound)
            {
                Cls_APPLICATION cls_APPLICATION = Cls_APPLICATION.FindByid(ApplicationID);
                if(cls_APPLICATION != null)
                {
                    return new Cls_LocaldrivngLisence(ApplicationID, cls_APPLICATION.PersonID, cls_APPLICATION.ApplicationDate, cls_APPLICATION.ApplicationTypeID, cls_APPLICATION.ApplicationStatus, cls_APPLICATION.LastStatusDate, cls_APPLICATION.PaidFees, cls_APPLICATION.CreatedByUserID, LOCALDRIVINGLISENCEID, ApplicationID, LicenseClassID);

                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }

        public byte NumberTestLocked()
        {
            return NumberTestLockedS(this.LOCALDRIVINGLISENCEID);

        }
        public static byte NumberTestLockedS(int LocalDrivingID)
        {
            return SQL_LOCALDRIVINGLISENCE.GetPassedTestCount(LocalDrivingID);

        }
        private bool _AddNewAPP()
        {
            this.LOCALDRIVINGLISENCEID = SQL_LOCALDRIVINGLISENCE.ADD_LOCALDRIVINGLISENCE(this.APPLICATIONID, LicenseClassID);
            return (this.LOCALDRIVINGLISENCEID != -1);

        }
        private bool _UpdateAPP()
        {
            return SQL_LOCALDRIVINGLISENCE.UPDATE_LOCALDRIVINGLISENCE(this.LOCALDRIVINGLISENCEID, this.APPLICATIONID, this.LicenseClassID);
        }

        public bool Save()
        {
            base._enmodeAPP = (Cls_APPLICATION.enmode)Mode;
            if(!base.SaveAPPLICATION())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAPP())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateAPP();

            }

            return false;

        }

        public static DataTable Getall()
        {
            return SQL_LOCALDRIVINGLISENCE.GETALL();
        }

        private static bool delete(int ID)
        {
            return SQL_LOCALDRIVINGLISENCE.delete(ID);
        }

        public  bool DeleteAll()
        {
            bool IsDeleted_LocalDrivingID = false;
            bool IsDeleted_ApplicationID = false;

            IsDeleted_LocalDrivingID  = Cls_LocaldrivngLisence.delete(this.LOCALDRIVINGLISENCEID);

            if (!IsDeleted_LocalDrivingID)
            {
                return false;
            }

           IsDeleted_ApplicationID = base.Delete();

            return IsDeleted_ApplicationID;



        }
        public bool Cancelled()
        {
            return StaticCancelled(this.LOCALDRIVINGLISENCEID);
        }

        public static bool StaticCancelled(int ID)
        {
            return SQL_LOCALDRIVINGLISENCE.Cancelled(ID);
        }

        public bool CheckIfPersonHasDemandeLocalDrivingLicenseBefore(int ApplicationID, int Types)
        {
            return SQL_LOCALDRIVINGLISENCE.CheckIfPersonHasDemandeLocalDrivingLicenseBefore(this.APPLICATIONID, this.ApplicationTypeID);
        }
        public static bool CheckIfPersonHasDemandeLocalDrivingLicenseBefore_Static(int ApplicationID, int Types)
        {
            return SQL_LOCALDRIVINGLISENCE.CheckIfPersonHasDemandeLocalDrivingLicenseBefore(ApplicationID, Types);
        }


    }
}
