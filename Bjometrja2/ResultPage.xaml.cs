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
using System.Windows.Shapes;

namespace Bjometrja2
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Window
    {
        List<List<PersonVector>> FirstVectorList;
        List<List<Person>> SecondVectorList;
        private int[] thresholds;
        public ResultPage()
        {
            InitializeComponent();
        }
        public ResultPage(int[] thresholds, List<List<PersonVector>> FirstVectorList, List<List<Person>> SecondVectorList)
        {
            InitializeComponent();
            this.thresholds = thresholds;
            this.FirstVectorList = FirstVectorList;
            this.SecondVectorList = SecondVectorList;
            insertValues();
        }

        private void insertValues()
        {
            List<object> items = new List<object>();
            List<object> items2 = new List<object>();
            for (int i = 0; i < 3; i++)
            {
                items.Add(new { column1 = thresholds[i], column2 = FirstVectorList[i][0].id, column3 = FirstVectorList[i][1].id, column4 = FirstVectorList[i][2].id });
                items2.Add(new { column1 = thresholds[i], column2 = SecondVectorList[i][0].id, column3 = SecondVectorList[i][1].id, column4 = SecondVectorList[i][2].id });
            }
            listViewFirstVector.ItemsSource = items;
            listViewSecondVector.ItemsSource = items2;
            CsvWriter.writeOutputToFile(FirstVectorList, SecondVectorList, "output.csv", thresholds);
        }
    }
}
