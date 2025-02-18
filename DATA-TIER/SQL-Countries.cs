using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_Countries
    {
        public static DataTable GetTable()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                connection.Close();

            }
            return dt;

        }
        public static bool GetCountryByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                    isFound = true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isFound = false;

            }
            finally
            {
                connection.Close();

            }
            return isFound;

        }
        public static bool GetCountryByName(string CountryName, ref int CountryID )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryID = (int)reader["CountryID"];
                    isFound = true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isFound = false;

            }
            finally
            {
                connection.Close();

            }
            return isFound;

        }



    }
}
