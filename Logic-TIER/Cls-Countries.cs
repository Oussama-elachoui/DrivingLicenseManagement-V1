using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Logic_TIER
{
    public class Cls_Countries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public Cls_Countries() 
        {
            CountryID=-1;
            CountryName = "";
        }

        public Cls_Countries(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static Cls_Countries FindById(int CountryID)
        {
            string CountryName = "";

            if (DATA_TIER.SQL_Countries.GetCountryByID(CountryID, ref CountryName))
            {
                return new Cls_Countries(CountryID, CountryName);
            }
            else
            {
                return null;
            }

        }
        public static Cls_Countries FindByCountryName(string CountryName)
        {
            int CountryID = -1;

            if (DATA_TIER.SQL_Countries.GetCountryByName(CountryName, ref CountryID))
            {
                return new Cls_Countries(CountryID, CountryName);
            }
            else
            {
                return null;
            }

        }

        public static DataTable GetAllcountries()
        {
            return DATA_TIER.SQL_Countries.GetTable();
        }


    }
}
