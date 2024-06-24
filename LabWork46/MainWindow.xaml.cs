using System.Data;
using System.Windows;

namespace LabWork46
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteNonQuery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = QueryTextBox.Text;
                int rowsAffected = DataAccessLayer.ExecuteNonQuery(query);
                OutputTextBox.Text = $"Количество измененных строк: {rowsAffected}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        private void UpdateBookPrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int BookId = int.Parse(BookIdTextBox.Text);
                double newPrice = double.Parse(NewPriceTextBox.Text);
                bool success = DataAccessLayer.UpdateBookPrice(BookId, newPrice);
                OutputTextBox.Text = $"Успешное обновление: {success}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        private void FetchDataTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tableName = TableNameTextBox.Text;
                DataTable dataTable = DataAccessLayer.ExecuteDataTable(tableName);
                if (dataTable != null)
                {
                    OutputTextBox.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                            OutputTextBox.AppendText(item + "\t");
                        OutputTextBox.AppendText(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        private void UpdateDataTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tableName = TableNameTextBox.Text;
                DataTable dataTable = DataAccessLayer.ExecuteDataTable(tableName);
                DataAccessLayer.UpdateDataTable(ref dataTable, tableName);
                OutputTextBox.Clear();
                OutputTextBox.AppendText("Таблица обновлена и повторно заполнена:" + Environment.NewLine);
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                        OutputTextBox.AppendText(item.ToString() + "\t");
                    OutputTextBox.AppendText(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }


        }
    }
}