using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GettingReal
{
    class Admin
    {   
        private static string connectionsString =
            "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public int CheckUserNameAndPassword(string userName, string password)
        {
            string userNameReceived = "";
            string passwordReceived = "";
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();

                    SqlCommand admin = new SqlCommand("spCheckCredentials", kNumberDB);
                    admin.CommandType = CommandType.StoredProcedure;
                    admin.Parameters.Add(new SqlParameter("@SentInUserName", userName));
                    admin.Parameters.Add(new SqlParameter("@SentInPassword", password));

                    SqlDataReader receivedUsernameAndPassword = admin.ExecuteReader();
                    while (receivedUsernameAndPassword.Read())
                    {
                        userNameReceived = receivedUsernameAndPassword.GetString(0);
                        passwordReceived = receivedUsernameAndPassword.GetString(1);
                    }
                    if (userName == userNameReceived)
                    {
                        if (password == passwordReceived)
                        {

                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return 1;
                }
            }
        }

        public int ChangePasswordInDB(string userName, string newPassword)
        {
            string hasPasswordUpdated = "";
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();

                    SqlCommand changePassword = new SqlCommand("spChangeAdminPassword", kNumberDB);
                    changePassword.CommandType = CommandType.StoredProcedure;
                    changePassword.Parameters.Add(new SqlParameter("@Username", userName));
                    changePassword.Parameters.Add(new SqlParameter("@UpdatePassword", newPassword));

                    changePassword.ExecuteNonQuery();

                    SqlCommand getPassword = new SqlCommand("spGetPassword", kNumberDB);
                    getPassword.CommandType = CommandType.StoredProcedure;
                    getPassword.Parameters.Add(new SqlParameter("@userName", userName));

                    SqlDataReader reader = getPassword.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            hasPasswordUpdated = reader["Pass"].ToString();
                        }
                    }
                    if (newPassword == hasPasswordUpdated)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }

                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return 1;
                }
            }
        }
    }
}
