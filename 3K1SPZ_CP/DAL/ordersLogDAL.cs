using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP.DAL
{
    public class ordersLogDAL
    {
        public static string GetProductNameByOrderID(int order_id)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"SELECT product_name FROM orders_log WHERE id = @id", connection);
                command.Parameters.Add(new SqlParameter("@id", order_id));
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        return (string)reader["product_name"];
                return "";
            }
        }

        public static List<Order> GetOrderHistoryOfUser(string login)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"SELECT
	                                                orders_log.id AS OrderID
                                                   ,users.login AS UserLogin
                                                   ,products.name AS ProductName
                                                   ,orders_log.order_time AS TimeOrdered
                                                FROM orders_log
                                                JOIN users
	                                                ON orders_log.user_login = users.login
                                                JOIN products
	                                                ON orders_log.product_name = products.name
                                                WHERE users.login = @login;", connection);
                command.Parameters.Add(new SqlParameter("@login", login));
                List<Order> orders = new();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order()
                        {
                            id = (int)reader["OrderID"],
                            user_login = (string)reader["UserLogin"],
                            product = (string)reader["ProductName"],
                            order_time = (DateTime)reader["TimeOrdered"]
                        });
                    }
                }
                return orders;
            }
        }

        public static void PlaceOrder(string user_login, string product_name)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"INSERT INTO orders_log (user_login, product_name) VALUES (@user_login, @product_name);", connection);
                command.Parameters.Add(new SqlParameter("@user_login", user_login));
                command.Parameters.Add(new SqlParameter("@product_name", product_name));
                command.ExecuteNonQuery();
            }
        }

        public static bool OrderIDToUserCheck(string user_login, int order_id)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"SELECT COUNT(*) FROM orders_log WHERE user_login = @user_login AND id = @order_id", connection);
                command.Parameters.Add(new SqlParameter("@user_login", user_login));
                command.Parameters.Add(new SqlParameter("@order_id", order_id));
                return (int)command.ExecuteScalar() == 1;
            }
        }
    }
}
