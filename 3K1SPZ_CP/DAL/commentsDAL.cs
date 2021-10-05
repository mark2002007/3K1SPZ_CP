using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP.DAL
{
    public class commentsDAL
    {
        public static void AddComment(int orderId, string commentText)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"INSERT INTO comments (order_id, text) VALUES (@order_id, @text);", connection);
                command.Parameters.Add(new SqlParameter("@order_id", orderId));
                command.Parameters.Add(new SqlParameter("@text", commentText));
                command.ExecuteNonQuery();
            }
        }

        public static List<Comment> GetCommentsOfUser(string login)
        {
            using (SqlConnection connection = new(Helper.CnnVal("DB")))
            {
                connection.Open();
                SqlCommand command = new(@"SELECT
	                                                comments.ID AS ID
                                                   ,comments.order_id AS OrderID
                                                   ,comments.text AS Text
                                                   ,comments.comment_time AS CommentTime
                                                FROM comments
                                                JOIN orders_log
	                                                ON comments.order_id = orders_log.ID
                                                WHERE orders_log.user_login = @login;", connection);
                command.Parameters.Add(new SqlParameter("@login", login));
                List<Comment> comments = new();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            id = (int)reader["ID"],
                            order_id = (int)reader["OrderID"],
                            text = (string)reader["Text"],
                            comment_time = (DateTime)reader["CommentTime"]
                        });
                    }
                }
                return comments;
            }
        }


    }
}
