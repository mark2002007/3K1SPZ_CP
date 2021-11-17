using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository() : base(Helper.CnnVal())
        {
        }
        public bool Create(UserDTO newUser)
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new(@"INSERT INTO users (login, password, disp_name) VALUES (@login, @password, @disp_name);", connection);
                    command.Parameters.Add(new SqlParameter("@login", newUser.Login));
                    command.Parameters.Add(new SqlParameter("@password", newUser.Password));
                    command.Parameters.Add(new SqlParameter("@disp_name", newUser.DispName));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<UserDTO> GetAll()
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM users", connection);
                List<UserDTO> users = new List<UserDTO>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        users.Add(new UserDTO()
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Password = (string)reader["password"],
                            DispName = (string)reader["disp_name"],
                            InsertTime = (DateTime)reader["insert_time"],
                            UpdateTime = (DateTime)reader["update_time"]
                        });
                }
                return users;
            }
        }
        public bool DeleteAll()
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new(@"DELETE FROM users;", connection);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public UserDTO Get(string login)
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM users WHERE login = @login", connection);
                command.Parameters.Add(new SqlParameter("@login", login));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        return new UserDTO()
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Password = (string)reader["password"],
                            DispName = (string)reader["disp_name"],
                            InsertTime = (DateTime)reader["insert_time"],
                            UpdateTime = (DateTime)reader["update_time"]
                        };
                }
                return null;
            }
        }
        public UserDTO Get(int id)
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM users WHERE id = @id", connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        return new UserDTO()
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Password = (string)reader["password"],
                            DispName = (string)reader["disp_name"],
                            InsertTime = (DateTime)reader["insert_time"],
                            UpdateTime = (DateTime)reader["update_time"]
                        };
                }
                return null;
            }
        }
        public bool UpdateDispName(string login, string newDispName)
        {

            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new(@"UPDATE users SET disp_name = @new_disp_name WHERE login = @login", connection);
                    command.Parameters.Add(new SqlParameter("@new_disp_name", newDispName));
                    command.Parameters.Add(new SqlParameter("@login", login));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePassword(string login, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new(@"UPDATE users SET password = @new_password WHERE login = @login", connection);
                    command.Parameters.Add(new SqlParameter("@new_password", newPassword));
                    command.Parameters.Add(new SqlParameter("@login", login));
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}