using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_LOCALDRIVINGLISENCE
    {
        public static int ADD_LOCALDRIVINGLISENCE(int ApplicationID, int LicenseClassID)
        {
            int personId = -1;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             INSERT INTO LOCALDRIVINGLISENCE 
                                 (ApplicationID, LicenseClassID) 
                                 VALUES 
                                       (@ApplicationID, @LicenseClassID);
                                       SELECT SCOPE_IDENTITY();";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            sqlCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool UPDATE_LOCALDRIVINGLISENCE(int LOCALDRIVINGLISENCEID, int ApplicationID, int LicenseClassID)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             UPDATE LOCALDRIVINGLISENCE 
                                 SET ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID
                                 WHERE LOCALDRIVINGLISENCEID = @LOCALDRIVINGLISENCEID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LOCALDRIVINGLISENCEID", LOCALDRIVINGLISENCEID);
            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            sqlCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static bool delete(int ID)
        {

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             DELETE FROM LOCALDRIVINGLISENCE 
                                 WHERE LOCALDRIVINGLISENCEID = @ID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static bool FindByid(int ID, ref int ApplicationID, ref int LicenseClassID)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             SELECT * FROM LOCALDRIVINGLISENCE 
                                 WHERE LOCALDRIVINGLISENCEID = @ID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    ApplicationID = Convert.ToInt32(sqlDataReader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(sqlDataReader["LicenseClassID"]);
                    return true;
                }
                else
                {
                    return false;
                }
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            return false;
        }

        public static DataTable GETALL()
        {

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             LocalDrivingLicenseApplications_View";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            DataTable dataTable = new DataTable();
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
}
