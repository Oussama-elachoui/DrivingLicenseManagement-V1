using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_APPLICATION_TYPES
    {
        public static int ADD_APPLICATION_TYPE(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplicationTypeID = -1;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"INSERT INTO ApplicationTypes (ApplicationTypeTitle, ApplicationFees) VALUES (@ApplicationTypeTitle, @ApplicationFees) SELECT SCOPE_IDENTITY()";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);



            sqlCommand.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            sqlCommand.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);


            try
            {
                object obj = sqlCommand.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int result))
                {
                    ApplicationTypeID = result;
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }

            return ApplicationTypeID;
        }

        public static bool UPDATE_APPLICATION_TYPE(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            sqlCommand.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }


        public static bool FindByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool IsFound = false;
            using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
            {
                string query = @"SELECT ApplicationTypeTitle, ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                try
                {
                    sqlConnection.Open(); // Ouvrir la connexion
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                        ApplicationFees = float.Parse(reader["ApplicationFees"].ToString());
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                }
            }
            return IsFound;
        }
        public static bool FindByTitle(string ApplicationTypeTitle, ref int ApplicationTypeID, ref float ApplicationFees)
        {
            bool Isfound = false;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"SELECT ApplicationTypeID, ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeTitle = @ApplicationTypeTitle";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

            try
            {
                sqlConnection.Open(); 
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationFees = float.Parse(reader["ApplicationFees"].ToString());
                    Isfound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();

            }
            return Isfound;

        }

        public static DataTable GetALLAPPLICATIONTABLE()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"SELECT * FROM ApplicationTypes";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return dt;

        }
    }
}
