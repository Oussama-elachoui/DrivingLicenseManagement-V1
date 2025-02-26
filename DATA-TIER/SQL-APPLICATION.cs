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
            string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @CreatedByUserID) SELECT SCOPE_IDENTITY()";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            sqlCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            sqlCommand.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);
            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                sqlConnection.Open();
                object result = sqlCommand.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    personId = ID;

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
        public static bool UPDATE_APPLICATION(int ApplicationID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"
        UPDATE Applications 
        SET ApplicantPersonID = @PersonID, 
            ApplicationDate = @ApplicationDate, 
            ApplicationTypeID = @ApplicationTypeID, 
            ApplicationStatus = @ApplicationStatus, 
            LastStatusDate = @LastStatusDate, 
            PaidFees = @PaidFees, 
            CreatedByUserID = @CreatedByUserID
        WHERE ApplicationID = @ApplicationID";

            using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating application", ex);
                }
            }

            return rowsAffected > 0;
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


        public static bool FindByApplicationID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                sqlConnection.Close();
            }

            return isFound;
        }

    }
}
