using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork45
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

        //5.2 SELECT MAX(Price) FROM Games
        public static object ExecuteScalar(string query)
        {

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            SqlCommand command = new(query, connection);

            return command.ExecuteScalar();
        }

        //5.3 SELECT * FROM Games
        public static DataTable ExecuteDataTable(string query)
        {

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            using SqlDataAdapter adapter = new(query, connection);

            DataTable table = new DataTable();

            table.Clear();
            adapter.Fill(table);

            return table;

        }

        //5.4 SELECT BookId, Title, Price FROM Book
        public static List<Book> GetBooks(string query)
        {
            List<Book> books = new();

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            SqlCommand command = new(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    Price = Convert.ToDouble(reader["Price"])
                };
                books.Add(book);
            }
            return books;
        }
    }
}
