using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class Cls_Application_Types
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        
       public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        public Cls_Application_Types(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            _Mode = enMode.Update;
        }
        public Cls_Application_Types()
        {
                
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0;
            _Mode = enMode.AddNew;

        }

        private bool _ADD()
        {
            this.ApplicationTypeID= DATA_TIER.SQL_APPLICATION_TYPES.ADD_APPLICATION_TYPE(this.ApplicationTypeTitle, this.ApplicationFees);

            return this.ApplicationTypeID != -1;
        }
        private bool _Update()
        {
            return DATA_TIER.SQL_APPLICATION_TYPES.UPDATE_APPLICATION_TYPE(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Update()
        {
            return _Update();
        }
        //public bool Save()
        //{
        //    switch (_Mode)
        //    {
        //        case enMode.AddNew:
        //            if (_ADD())
        //            {

        //                _Mode = enMode.Update;
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        case enMode.Update:

        //            return _Update();

        //    }

        //    return false;
        //}

        public static DataTable GetAll()
        {
            return DATA_TIER.SQL_APPLICATION_TYPES.GetALLAPPLICATIONTABLE();
        }

        public static Cls_Application_Types FindByID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;

            if(DATA_TIER.SQL_APPLICATION_TYPES.FindByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new Cls_Application_Types(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

        public static Cls_Application_Types FindBytitle(string ApplicationTypeTitle)
        {
            int ApplicationTypeID = -1;
            float ApplicationFees = 0;

            if (DATA_TIER.SQL_APPLICATION_TYPES.FindByTitle(ApplicationTypeTitle, ref ApplicationTypeID, ref ApplicationFees))
            {
                return new Cls_Application_Types(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }



    }
}
