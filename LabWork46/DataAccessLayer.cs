using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork46
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

        //5.1 "UPDATE Games SET price = price + 1"
        public static int ExecuteNonQuery(string query)
        {

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            SqlCommand command = new(query, connection);

            return command.ExecuteNonQuery();


        }

        //5.2 
        public static bool UpdateBookPrice(int BookId, double newPrice)
        {
            string query = $"UPDATE Book SET Price = {newPrice} WHERE BookId = {BookId}";

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            SqlCommand command = new(query, connection);

            return command.ExecuteNonQuery() > 0;
        }

        //5.3
        public static DataTable ExecuteDataTable(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            using SqlDataAdapter adapter = new(query, connection);

            DataTable dataTable = new();
            adapter.Fill(dataTable);

            return dataTable;
        }

        //5.4
        public static void UpdateDataTable(ref DataTable dataTable, string tableName)
        {
            string query = $"SELECT * FROM {tableName}";

            using SqlConnection connection = new(GetConnectionString());
            connection.Open();

            using SqlDataAdapter adapter = new(query, connection);

            adapter.Update(dataTable);
            dataTable.Clear();
            adapter.Fill(dataTable);
        }
    }
}
