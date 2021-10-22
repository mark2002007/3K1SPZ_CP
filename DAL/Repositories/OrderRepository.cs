using System.Data.SqlClient;
using _3K1SPZ_CP.DAL.Repositories;

namespace _3K1SPZ_CP.DAL;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public void CreateOrder(int userId, int productId)
    {
        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command =
                new(@"INSERT INTO orders_log (user_id, product_id) VALUES (@user_id, @product_id);",
                    connection);
            command.Parameters.Add(new SqlParameter("@user_id", userId));
            command.Parameters.Add(new SqlParameter("@product_id", productId));
            command.ExecuteNonQuery();
        }
    }
    public OrderDTO Get(int id)
    {
        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(@"SELECT
	                                                orders_log.id AS id
                                                   ,user_id AS user_id
                                                   ,products.id AS product_id
                                                   ,products.name AS product_name
                                                   ,orders_log.insert_time AS insert_time
                                                   ,orders_log.update_time AS update_time
                                                FROM orders_log
                                                JOIN products
	                                                ON orders_log.product_id = products.id
                                                WHERE orders_log.id = @id", connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    return new OrderDTO
                    {
                        Id = (int)reader["id"],
                        UserId = (int)reader["user_id"],
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        InsertTime = (DateTime)reader["insert_time"],
                        UpdateTime = (DateTime)reader["update_time"]
                    };
            }

            return null;
        }
    }
    public List<OrderDTO> GetAll()
    {
        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(@"SELECT
	                                                orders_log.id AS id
                                                   ,user_id AS user_id
                                                   ,products.id AS product_id
                                                   ,products.name AS product_name
                                                   ,orders_log.insert_time AS insert_time
                                                   ,orders_log.update_time AS update_time
                                                FROM orders_log
                                                JOIN products
	                                                ON orders_log.product_id = products.id", connection);
            List<OrderDTO> orders = new List<OrderDTO>();
            using (var reader = command.ExecuteReader())
                while (reader.Read())
                    orders.Add(new OrderDTO
                    {
                        Id = (int)reader["id"],
                        UserId = (int)reader["user_id"],
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        InsertTime = (DateTime)reader["insert_time"],
                        UpdateTime = (DateTime)reader["update_time"]
                    });

            return orders;
        }
    }
    public bool DeleteAll()
    {
        try
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new(@"DELETE FROM orders;", connection);
                command.ExecuteNonQuery();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public OrderRepository(string connectionString) : base(connectionString)
    {
    }
    public List<OrderDTO> GetOrderHistory(int userId)
    {
        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(@"SELECT
	                                                orders_log.id AS id
                                                   ,products.id AS product_id
                                                   ,products.name AS product_name
                                                   ,orders_log.insert_time AS insert_time
                                                   ,orders_log.update_time AS update_time
                                                FROM orders_log
                                                JOIN products
	                                                ON orders_log.product_id = products.id
                                                WHERE user_id = @id", connection);
            command.Parameters.Add(new SqlParameter("@id", userId));
            List<OrderDTO> orders = new();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    orders.Add(new OrderDTO
                    {
                        Id = (int)reader["id"],
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        InsertTime = (DateTime)reader["insert_time"],
                        UpdateTime = (DateTime)reader["update_time"]
                    });
            }

            return orders;
        }
    }
    public bool OrderIDToUserCheck(int userId, int orderId)
    {
        OrderRepository orderRepository = new OrderRepository(Helper.CnnVal());
        return orderRepository.Get(orderId).UserId == userId;
    }
}