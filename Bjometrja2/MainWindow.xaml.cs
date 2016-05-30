using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var lista = dbconnector.SelectByID(176);
            foreach (List<string> item in lista)
            {
                foreach (string elo in item)
                {
                    Console.WriteLine(elo);
                }
            }
            MessageBox.Show("done");
        }
    }
}
