

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
                             INSERT INTO LocalDrivingLicenseApplications 
                                 (ApplicationID, LicenseClassID) 
                                 VALUES 
                                       (@ApplicationID, @LicenseClassID);
                                       SELECT SCOPE_IDENTITY();";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            sqlCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                sqlConnection.Open();
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
            int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             UPDATE LocalDrivingLicenseApplications 
                                 SET ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID
                                 WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LOCALDRIVINGLISENCEID);
            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            sqlCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                sqlConnection.Open();
                rowsAffected=sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return rowsAffected > 0;
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

        public static bool FindByid(
            int LocalDrivingLicenseApplicationID, ref int ApplicationID,
            ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);


            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];



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
        public static DataTable GETALL()
        {
            DataTable dt = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             select * from LocalDrivingLicenseApplications_View";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader Reader = sqlCommand.ExecuteReader();
                if (Reader.Read())
                {
                    dt.Load(Reader);
                }
                Reader.Close();

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dt;
        }

        public static bool CheckIfPersonHasDemandeLocalDrivingLicenseBefore(int PersonID,int Types)
        {
            bool isFound = false;

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             select Applications.ApplicantPersonID , LocalDrivingLicenseApplications.LicenseClassID from Applications inner join LocalDrivingLicenseApplications on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID where Applications.ApplicantPersonID = @ApplicantPersonID and LocalDrivingLicenseApplications.LicenseClassID=@LicenseClassID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@LicenseClassID", Types);

            try
            {
                sqlConnection.Open();
                SqlDataReader Reader = sqlCommand.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                }
                Reader.Close();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            return isFound;
        }


        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(StringConnection.connectionString);

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;



        }

        public static bool Cancelled(int LocalId)
        {
            int rowsAffected = 0;
            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             update Applications set Applications.ApplicationStatus=2 from Applications inner join LocalDrivingLicenseApplications on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID 
                             where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalId);


            try
            {
                sqlConnection.Open();
                rowsAffected = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();

            }
            return rowsAffected > 0;
        }

        public static bool DoesAttendByTest(int localId, int testType)
        {
            bool isFound = false;

            using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
            {
                string query = @"
            SELECT TestAppointments.LocalDrivingLicenseApplicationID, TestAppointments.TestAppointmentID 
            FROM TestAppointments 
            INNER JOIN Tests 
            ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
            WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
            AND TestAppointments.TestTypeID = @TestTypeID
            AND Tests.TestResult = 1";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localId);
                    sqlCommand.Parameters.AddWithValue("@TestTypeID", testType);

                    try
                    {
                        sqlConnection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return isFound;
        }

        public static bool DoesattendBytest(int localId,int testtype)
        {
            bool isFound = false;

            SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString);
            string query = @"
                             select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localId);
            sqlCommand.Parameters.AddWithValue("@TestTypeID", testtype);

            try
            {
                sqlConnection.Open();
                SqlDataReader Reader = sqlCommand.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                }
                Reader.Close();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            return isFound;
        }
        public static bool DoesAttendTestSucced(int localId, int testType)
        {
            bool isFound = false;

            using (SqlConnection sqlConnection = new SqlConnection(StringConnection.connectionString))
            {
                string query = @"
            SELECT TestAppointments.LocalDrivingLicenseApplicationID, TestAppointments.TestAppointmentID 
            FROM TestAppointments 
            INNER JOIN Tests 
            ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
            WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
            AND TestAppointments.TestTypeID = @TestTypeID
            AND Tests.TestResult = 1";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localId);
                    sqlCommand.Parameters.AddWithValue("@TestTypeID", testType);

                    try
                    {
                        sqlConnection.Open();
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return isFound;
        }

    }
}
