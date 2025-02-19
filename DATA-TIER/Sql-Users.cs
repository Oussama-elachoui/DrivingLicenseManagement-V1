using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class Sql_Users
    {
        public static int ADD_USER(int PeronsId, string UserName, string Password, bool isActive)
        {
            int UserID = 1;

            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "insert into Users (PersonID,UserName,Password,IsActive) values (@PersonID,@UserName,@Password,@isActive) SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PeronsId);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);

            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int result))
                {
                    UserID = result;
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }
        public static bool Update_USER(int UserID, int PeronsId, string UserName, string Password, bool isActive)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "update Users set PersonID=@PersonID,UserName=@UserName,Password=@Password,IsActive=@isActive where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PeronsId);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);

        }

        public static bool Delete(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "delete from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetallUsers()
        {
            DataTable Dt = new DataTable();
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select Fullname=FirstName+' '+SecondName+' '+ThirdName+' '+LastName , UserName,Email , Phone , case  WHEN Gendor=0 then 'Male' when Gendor=1 then 'Female' end as Gendor ,CountryName,IsActive\r\nfrom People inner join Countries on Countries.CountryID=People.NationalityCountryID inner join Users on Users.PersonID=People.PersonID;";
            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Dt.Load(reader);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Dt;
        }

        public static bool GetUserById(int UserID, ref int PeronsId, ref string UserName, ref string Password, ref bool isActive)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    PeronsId = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
        public static bool GetUserByPersonId(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool isActive)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static bool IsExistUserName(string UserName)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where UserName=@UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
        public static bool IsExistByPersonId(int PersonID)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
        public static bool IsExistByUsernameAndPassword(string UserName, string Password)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where UserName=@UserName and Password=@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
        public static bool IsExistByUserId(string UserID)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * from Users where UserID=@UserID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isExist = true;
                }

            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
    }
}
