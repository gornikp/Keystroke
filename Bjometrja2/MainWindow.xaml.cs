using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace Bjometrja2
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DBConnect dbconnector = new DBConnect();
            DataTable lista = dbconnector.SelectAll();
            //foreach (List<string> item in lista)
            //{
            //    foreach (string elo in item)
            //    {
            //        Console.WriteLine(elo);
            //    }
            //}
            dataGrid.AutoGenerateColumns = true;
            dataGrid.ItemsSource = lista.DefaultView;
            MessageBox.Show("done");
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
