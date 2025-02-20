using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class Cls_TestTypes
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public Cls_TestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            _Mode = enMode.Update;
        }
        public Cls_TestTypes()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
            _Mode = enMode.AddNew;
        }

        public static Cls_TestTypes Find(int Id)
        {
            string TestTypeTitle ="", TestTypeDescription = "";
            float TestTypeFees = 0;

            if(DATA_TIER.SQL_TestTypes.TestTypesManager.FindByID(Id, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new Cls_TestTypes(Id, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }
        private bool _ADD()
        {
            this.TestTypeID = DATA_TIER.SQL_TestTypes.TestTypesManager.ADD_TEST_TYPE(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return this.TestTypeID != -1;
        }
        private bool _Update()
        {
            return DATA_TIER.SQL_TestTypes.TestTypesManager.UPDATE_TEST_TYPE(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Update()
        {
            return _Update();
        }

        public static DataTable Getalltable()
        {
            return DATA_TIER.SQL_TestTypes.TestTypesManager.GetAllTestTypes();
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
        //        default:
        //            return false;
        //    }
        //}
    }
}
