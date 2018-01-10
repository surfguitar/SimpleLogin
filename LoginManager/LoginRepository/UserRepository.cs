using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using System.Data.SqlClient;

namespace LoginRepository
{
    public class UserRepository
    {
        const string connectionString = "Server=tcp:test-gus.database.windows.net,1433;Initial Catalog=ChatDb;Persist Security Info=False;User ID=gustav;Password=Lunatones666;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int CreateUser(User user)
        {
            string query = "INSERT Into [User] ( Name, Password, Email, CreatedDate ) " +
                    "VALUES ( @Name, @Password, @Email, @CreatedDate ) SELECT CAST(scope_identity() AS int) ";

            // instance connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // add parameters and their values
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);

                
                connection.Open();
                int id = (int)cmd.ExecuteScalar();
                return id;
            }
        }

        public User GetUser(string name, string password)
        {
            string sql = "SELECT * FROM [User] WHERE Name = @Name AND Password = @Password";
            User user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        // Now you're pointed at the first result row
                        user = new User
                        {
                            Id = (int)dr["Id"],
                            Name = dr["Name"].ToString(),
                            Email = dr["Email"].ToString()
                        };
                    }
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
            return user;
        }
    }
}
