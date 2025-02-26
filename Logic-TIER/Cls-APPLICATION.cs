using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class Cls_APPLICATION
    {
        public int APPLICATIONID { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public string StatusText
        {
            get
            {
                 
                switch (ApplicationStatus)
                {
                    case 1:
                        return "New";
                    case 2:
                        return "Cancelled";
                    case 3:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }

        protected enum enmode
        {
            Add = 0,
            Update = 1
        }
        protected enmode _enmodeAPP = enmode.Add;

        public Cls_APPLICATION(int APPLICATIONID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.APPLICATIONID = APPLICATIONID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            _enmodeAPP = enmode.Update;
            this.CreatedByUserID = CreatedByUserID;
        }

        public Cls_APPLICATION()
        {

            this.APPLICATIONID = -1;
            this.PersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            _enmodeAPP = enmode.Add;


        }
        
        private bool _AddNewAPP()
        {

                this.APPLICATIONID = DATA_TIER.SQL_APPLICATION.ADD_APPLICATION(this.PersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
                return (this.APPLICATIONID != -1);

        }
        private bool Updatte()
        {

            return DATA_TIER.SQL_APPLICATION.UPDATE_APPLICATION(this.APPLICATIONID, this.PersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool Delete()
        {
            return DeleteS(this.APPLICATIONID);
        }
        public static bool DeleteS(int APPLICATIONID)
        {
            return DATA_TIER.SQL_APPLICATION.DELETE_APPLICATION(APPLICATIONID);
        }
        public bool SaveAPPLICATION()
        {
            switch (this._enmodeAPP)
            {
                case enmode.Add:
                    if (_AddNewAPP())
                    {

                        _enmodeAPP = enmode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enmode.Update:

                    return Updatte();

            }

            return false;
        }


        public static Cls_APPLICATION FindByid(int APPLICATIONID)
        {
            int Personid = -1, ApplicationTypeID = -1, CreatedByUserID = -1; byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;

            bool IsFound = DATA_TIER.SQL_APPLICATION.FindByApplicationID(APPLICATIONID,ref Personid,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees,ref CreatedByUserID);
            if (IsFound)
            {
                return new Cls_APPLICATION(APPLICATIONID, Personid, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }


    }
}
