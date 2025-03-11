using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SQL_DRIVERS
    {
        public static int ADD(int personID, int CreatedBy, DateTime CreatedDate)
        {
            int ID = -1;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string qeury = @"INSERT INTO Drivers (PersonID,CreatedByUserID,CreatedDate) VALUES(@PersonID,@CreatedByUserID,@CreatedDate) @ scoup_identity()";
            SqlCommand sqlCommand = new SqlCommand(qeury, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", personID);
            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedBy);
            sqlCommand.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                object obj = sqlCommand.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int result))
                {
                    ID = result;
                }


            }
            catch (Exception)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return ID;
        }
        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                sqlConnection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                sqlConnection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool getDriverByID(int ID, ref int personID, ref int CreatedBy, ref DateTime CreatedDate)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string qeury = @"SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand sqlCommand = new SqlCommand(qeury, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@DriverID", ID);

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    personID = (int)reader["PersonID"];
                    CreatedBy = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    result = true;
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static bool getDriverByPersonID(int personID, ref int DriverID, ref int CreatedBy, ref DateTime CreatedDate)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string qeury = @"SELECT * FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand sqlCommand = new SqlCommand(qeury, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
                    CreatedBy = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    result = true;
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }

        public static DataTable GetALL()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string qeury = @"SELECT * FROM Drivers";
            SqlCommand sqlCommand = new SqlCommand(qeury, sqlConnection);

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception)
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
