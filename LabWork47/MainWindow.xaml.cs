using System.Data;
using System.Windows;

namespace LabWork47
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

        //5.1
        private void GetBookCountButton_Click(object sender, RoutedEventArgs e)
        { 
            decimal price = decimal.Parse(PriceTextBox.Text);
            int success = DataAccessLayer.GetBookCountByPrice(price);
            BookCountResultTextBlock.Text = $"Успешное обновление: {success}";
        }

        //5.2
        private void InsertBookButton_Click(object sender, RoutedEventArgs e)
        {
            string command = InsertCommandTextBox.Text;
            int id = DataAccessLayer.InsertBookAndGetId(command);
            InsertResultTextBlock.Text = id >= 0 ? $"ID вставленной книги: {id}" : "Ошибка при вставке книги.";
        }

        //5.3
        private void GetBooksButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PriceForGenreTextBox.Text, out decimal price) && !string.IsNullOrEmpty(GenreTextBox.Text))
            {
                DataTable books = DataAccessLayer.GetBooksByPriceAndGenre(price, GenreTextBox.Text);
                BooksDataGrid.ItemsSource = books?.DefaultView;
            }
            else
            {
                PriceForGenreTextBox.Text = "неправильно введены данные";
            }
        }

        //5.4
        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BookIdTextBox.Text, out int id) && decimal.TryParse(NewPriceTextBox.Text, out decimal price) && !string.IsNullOrEmpty(NewTitleTextBox.Text))
            {
                bool success = DataAccessLayer.UpdateBook(id, price, NewTitleTextBox.Text);
                UpdateResultTextBlock.Text = success ? "Книга успешно обновлена." : "Ошибка при обновлении книги.";
            }
            else
            {
                UpdateResultTextBlock.Text = "Неправильнный ввод";
            }
        }
    }
}