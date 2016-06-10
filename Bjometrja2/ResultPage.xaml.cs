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
        }     
    }
}
