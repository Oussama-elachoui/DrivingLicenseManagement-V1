using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_TestTypes
    {
        public static class TestTypesManager
        {
            public static int ADD_TEST_TYPE(string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
            {
                int TestTypeID = -1;
                using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
                {
                    string query = @"INSERT INTO TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees)
                             VALUES (@TestTypeTitle, @TestTypeDescription, @TestTypeFees);
                             SELECT SCOPE_IDENTITY();";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    sqlCommand.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    sqlCommand.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

                    try
                    {
                        sqlConnection.Open();
                        object obj = sqlCommand.ExecuteScalar();
                        if (obj != null && int.TryParse(obj.ToString(), out int result))
                        {
                            TestTypeID = result;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return TestTypeID;
            }

            public static bool UPDATE_TEST_TYPE(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
            {
                bool result = false;
                using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
                {
                    string query = @"UPDATE TestTypes 
                             SET TestTypeTitle = @TestTypeTitle, 
                                 TestTypeDescription = @TestTypeDescription, 
                                 TestTypeFees = @TestTypeFees 
                             WHERE TestTypeID = @TestTypeID";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    sqlCommand.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    sqlCommand.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    sqlCommand.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

                    try
                    {
                        sqlConnection.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return result;
            }

            public static bool FindByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
            {
                bool isFound = false;
                using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
                {
                    string query = @"SELECT TestTypeTitle, TestTypeDescription, TestTypeFees 
                             FROM TestTypes 
                             WHERE TestTypeID = @TestTypeID";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            TestTypeTitle = reader["TestTypeTitle"].ToString();
                            TestTypeDescription = reader["TestTypeDescription"].ToString();
                            TestTypeFees = float.Parse(reader["TestTypeFees"].ToString());
                            isFound = true;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return isFound;
            }

            public static DataTable GetAllTestTypes()
            {
                DataTable dt = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
                {
                    string query = @"SELECT * FROM TestTypes";
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
                        Console.WriteLine(ex.Message);
                    }
                }
                return dt;
            }
        }

    }
}
