using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_TIER
{
    public class SqlPeople
    {
        public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int result = -1;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "insert into People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath) " +
                "values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath) SELECT SCOPE_IDENTITY();";

            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            Cmd.Parameters.AddWithValue("@FirstName", FirstName);
            Cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "" && ThirdName != null)
                Cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Cmd.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            Cmd.Parameters.AddWithValue("@LastName", LastName);
            Cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Cmd.Parameters.AddWithValue("@Gendor", Gendor);
            Cmd.Parameters.AddWithValue("@Address", Address);
            Cmd.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                Cmd.Parameters.AddWithValue("@Email", Email);
            else
                Cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);

            Cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                Cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                sqlConnection.Open();
                Object obj = Cmd.ExecuteScalar();

                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                    result = ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;


        }
        public static bool Update(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int RowAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "UPDATE People SET " +
                   "NationalNo = @NationalNo, " +
                   "FirstName = @FirstName, " +
                   "SecondName = @SecondName, " +
                   "ThirdName = @ThirdName, " +
                   "LastName = @LastName, " +
                   "DateOfBirth = @DateOfBirth, " +
                   "Gendor = @Gendor, " +
                   "Address = @Address, " +
                   "Phone = @Phone, " +
                   "Email = @Email, " +
                   "NationalityCountryID = @NationalityCountryID, " +
                   "ImagePath = @ImagePath " +
                   "WHERE PersonID = @PersonID;";

            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@PersonID", PersonID);
            Cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            Cmd.Parameters.AddWithValue("@FirstName", FirstName);
            Cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "" && ThirdName != null)
                Cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Cmd.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            Cmd.Parameters.AddWithValue("@LastName", LastName);
            Cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Cmd.Parameters.AddWithValue("@Gendor", Gendor);
            Cmd.Parameters.AddWithValue("@Address", Address);
            Cmd.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                Cmd.Parameters.AddWithValue("@Email", Email);
            else
                Cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);

            Cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                Cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);



            try
            {
                sqlConnection.Open();
                RowAffected = Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                RowAffected = 0; ;
            }
            finally
            {
                sqlConnection.Close();
            }

            return (RowAffected > 0);


        }

        public static bool Delete(int PersonID)
        {
            int RowAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID;";

            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                sqlConnection.Open();
                RowAffected = Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                RowAffected = 0; ;
            }
            finally
            {
                sqlConnection.Close();
            }

            return (RowAffected > 0);

        }

        public static bool GetPersonByID(int PersonID, ref string rNationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE PersonID = @PersonID;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    rNationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static bool GetPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE NationalNo = @NationalNo;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static bool IsExistNationalNo(string NationalNo)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE NationalNo = @NationalNo;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();

            }
            return false;
        }
        public static bool IsExistEmail(string Email)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE Email = @Email;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@Email", Email);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();

            }
            return false;
        }

        public static bool IsExistPhone(string Phone)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE Phone = @Phone;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();

            }
            return false;
        }
        public static bool IsExistPersonID(int PersonID)
        {
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = "Select * FROM People WHERE PersonID = @PersonID;";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);
            Cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnection.Close();

            }
            return false;
        }



        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);

            string query =
                          @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gendor,  
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";
            SqlCommand Cmd = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

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
                sqlConnection.Close();
            }

            return dt;


        }
    }
}


