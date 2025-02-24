using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_APPLICATION
    {
        public static int ADD_APPLICATION(int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int personId = -1;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             INSERT INTO APPLICATION 
                                 (PersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) 
                                 VALUES 
                                       (@PersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                                       SELECT SCOPE_IDENTITY();";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            sqlCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            sqlCommand.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);
            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                object result = sqlCommand.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    personId = ID;

                }
                else
                {
                    return -1;
                }


            }

            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return personId;


        }
        public static bool UPDATE_APPLICATION(int APPLICATIONID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             UPDATE APPLICATION 
                                 SET PersonID = @PersonID, ApplicationDate = @ApplicationDate, ApplicationTypeID = @ApplicationTypeID, ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID
                                 WHERE APPLICATIONID = @APPLICATIONID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@APPLICATIONID", APPLICATIONID);
            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            sqlCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            sqlCommand.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);
            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                sqlConnection.Open();
                RowsAffected = sqlCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return RowsAffected > 0;
        }

        public static bool DELETE_APPLICATION(int APPLICATIONID)
        {
            int RowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             DELETE FROM APPLICATION 
                                 WHERE APPLICATIONID = @APPLICATIONID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@APPLICATIONID", APPLICATIONID);
            try
            {
                sqlConnection.Open();
                RowsAffected = sqlCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return RowsAffected > 0;
        }

        public static bool FindByApplicationID(int APPLICATIONID, ref int PersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             SELECT PersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID
                                 FROM APPLICATION 
                                 WHERE APPLICATIONID = @APPLICATIONID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@APPLICATIONID", APPLICATIONID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    PersonID = (int)sqlDataReader["PersonID"];
                    ApplicationDate = (DateTime)sqlDataReader["ApplicationDate"];
                    ApplicationTypeID = (int)sqlDataReader["ApplicationTypeID"];
                    ApplicationStatus = (byte)sqlDataReader["ApplicationStatus"];
                    LastStatusDate = (DateTime)sqlDataReader["LastStatusDate"];
                    PaidFees = (decimal)sqlDataReader["PaidFees"];
                    CreatedByUserID = (int)sqlDataReader["CreatedByUserID"];

                    return true;
                }
                sqlDataReader.Close();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return false;

        }


    }
}
