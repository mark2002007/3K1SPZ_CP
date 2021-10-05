using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP.DAL
{
    public class userDAL
    {
        public static bool CheckPassword(string login, string password)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new("SELECT COUNT(*) FROM users WHERE login = @login AND password = @password", connection);
                command.Parameters.Add(new SqlParameter("@login", login));
                command.Parameters.Add(new SqlParameter("@password", password));
                return (int)command.ExecuteScalar() == 1;
            }
        }

        public static (string, string, string) GetUserInfo(string user_login)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM users WHERE login = @login", connection);
                command.Parameters.Add(new SqlParameter("@login", user_login));
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        return ((string)reader["login"], (string)reader["password"], (string)reader["disp_name"]);
                return ("", "", "");
            }
        }

        public static void ChangeDispNameByLogin(string login, string newDispName)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"UPDATE users SET disp_name = @new_disp_name WHERE login = @login", connection);
                command.Parameters.Add(new SqlParameter("@new_disp_name", newDispName));
                command.Parameters.Add(new SqlParameter("@login", login));
                command.ExecuteNonQuery();
            }
        }

        public static void ChangePasswordByLogin(string login, string newPassword)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"UPDATE users SET password = @new_password WHERE login = @login", connection);
                command.Parameters.Add(new SqlParameter("@new_password", newPassword));
                command.Parameters.Add(new SqlParameter("@login", login));
                command.ExecuteNonQuery();
            }
        }
    }
}
