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
        public int LOCALDRIVINGLISENCEID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }



        public Cls_LocaldrivngLisence(int APPLICATIONID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int LOCALDRIVINGLISENCEID, int ApplicationID, int LicenseClassID)
        {
            this.LOCALDRIVINGLISENCEID = LOCALDRIVINGLISENCEID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.APPLICATIONID = APPLICATIONID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this._enmodeAPP= enmode.Update;
        }

        public Cls_LocaldrivngLisence()
        {

            this.LOCALDRIVINGLISENCEID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            this.APPLICATIONID = -1;
            this.PersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            this._enmodeAPP = enmode.Update;

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

        private bool _AddNewAPP()
        {

            int Id = SQL_LOCALDRIVINGLISENCE.ADD_LOCALDRIVINGLISENCE(this.APPLICATIONID, LicenseClassID);
            if (Id != -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool _UpdateAPP()
        {
            return SQL_LOCALDRIVINGLISENCE.UPDATE_LOCALDRIVINGLISENCE(this.LOCALDRIVINGLISENCEID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Save()
        {
            switch (this._enmodeAPP)
            {
                case enmode.Add:
                    if (this.SaveAPPLICATION())
                    {
                        if (_AddNewAPP())
                        {
                            _enmodeAPP = enmode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                case enmode.Update:
                    return this.SaveAPPLICATION() & _UpdateAPP();
                default:
                    return false;
            }
        }

        public static DataTable Getall()
        {
            return SQL_LOCALDRIVINGLISENCE.GETALL();
        }
    }
}
