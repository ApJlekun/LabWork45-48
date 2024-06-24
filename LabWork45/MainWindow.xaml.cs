using System.Windows;

namespace LabWork45
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GetConnectionString.Content = DataAccessLayer.GetConnectionString();
        }

        private void ExecuteScalar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = ScalarCommandTextBox.Text;
                var scalarResult = DataAccessLayer.ExecuteScalar(query);
                ScalarResultLabel.Content = $"Результат ExecuteScalar: {scalarResult}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        private void ExecuteDataTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = DataTableCommandTextBox.Text;
                var table = DataAccessLayer.ExecuteDataTable(query);
                if (table != null)
                    DataTableGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }


        }

        private void GetBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = BooksCommandTextBox.Text;
                var books = DataAccessLayer.GetBooks(query);
                BooksGrid.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
    }
}