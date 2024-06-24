using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork47
{
    public static class DataAccessLayer
    {
        //5.1
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = @"prserver\SQLEXPRESS", // server
                UserID = "ispp2108", // login
                Password = "2108", // Password
                InitialCatalog = "ispp2108", // BD
                TrustServerCertificate = true
            };
            return builder.ConnectionString;
        }

        //5.1 
        public static int GetBookCountByPrice(decimal price)
        {
            try
            {
                using SqlConnection connection = new(GetConnectionString());
                connection.Open();

                string query = "SELECT * FROM Book WHERE Price < @Price";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Price", price);

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return -1;
            }
        }

        //5.2
        public static int InsertBookAndGetId(string insertCommand)
        {                
            string query = $"{insertCommand} SET @id = SCOPE_IDENTITY()";

            try
            {
                using SqlConnection connection = new(GetConnectionString());
                connection.Open();

                using SqlCommand command = new(query, connection);
                SqlParameter idParameter = new("@id", Id);
                idParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParameter);

                command.ExecuteNonQuery();
                return (int)idParameter.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return -1;
            }
        }

        //5.3
        public static DataTable GetBooksByPriceAndGenre(decimal price, string genre)
        {                
            string query = "SELECT * FROM Books WHERE Price < @Price AND Genre = @Genre";

            try
            {
                using SqlConnection connection = new(GetConnectionString());
                connection.Open();

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Genre", genre);

                SqlDataAdapter adapter = new(command);
                DataTable result = new();
                adapter.Fill(result);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }
        }

        //5.4
        public static bool UpdateBook(int id, decimal price, string title)
        {
            string query = "UPDATE Book SET Price = @Price, Title = @Title WHERE Id = @BookId";

            try
            {
                using SqlConnection connection = new(GetConnectionString());
                connection.Open();

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@BookId", id);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Title", title);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }
    }
} 



