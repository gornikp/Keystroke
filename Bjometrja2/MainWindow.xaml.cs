using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bjometrja2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<long[]> vectors1;
        List<long[]> vectors2;
        Stopwatch timer;
        Stopwatch timer2;
        public long[] ButtonDownTime;
        public long[] ButtonDownCount;
        DateTime buttonDown;
        DateTime spaceDown;
        TimeSpan sapceTime;
        TimeSpan sapceTime2;
        bool previousWasSpace;
        int spaceCounter;
        int spaceCounter2;
        List<Person> persons;
        DataProcessing dataProcessing;
        public MainWindow()
        {
            InitializeComponent();
            restartValues();
        }
        private void restartValues ()
        {
            textBox.IsEnabled = false;
            textBoxFirst.IsEnabled = true;
            textBoxSecond.IsEnabled = true;
            textBoxThird.IsEnabled = true;
            vectors1 = new List<long[]>(3);
            vectors2 = new List<long[]>(3);
            timer = new Stopwatch();
            timer2 = new Stopwatch();
            ButtonDownTime = new long[26];
            ButtonDownCount = new long[26];
            sapceTime = new TimeSpan();
            sapceTime2 = new TimeSpan();
            previousWasSpace = false;
            for (int i = 0; i < 3; i++)
            {
                vectors1.Add(new long[26]);
                vectors2.Add(new long[4]);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            dataProcessing = new DataProcessing(new DBConnect());
            restartValues();
            List<InputData> id = dataProcessing.getFirstVectorByUserId(200);
            //DBConnect dbconnector = new DBConnect();
            //DataTable lista = dbconnector.SelectAll();
            //string elo = lista.Rows[3].ItemArray[3].ToString();// pobiera input 1
            //string[] splittedstring = elo.Split(' ');
            //List<string[]> splied = new List<string[]>();
            //foreach (var item in splittedstring)
            //{
            //    splied.Add(item.Split('_'));
            //}
            //foreach (string[] keyEvent in splied)
            //{
            //    foreach (var item in keyEvent)
            //    {
            //        if(item[0] == 'd')
            //        {

            //        }
            //    }
            //}
            //dataGrid.AutoGenerateColumns = true;
            //dataGrid.ItemsSource = lista.DefaultView;
            Console.WriteLine(vectors1);
            Console.WriteLine(vectors2);
        }
        
        private void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!timer.IsRunning)
                timer.Start();
            timer2.Restart();
            if (!(e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space))
            {
                e.Handled = true;
            } 
            if(previousWasSpace)
            {
                buttonDown = DateTime.Now;
                sapceTime2 += buttonDown.Subtract(spaceDown);
                spaceCounter2++;
            }
            previousWasSpace = false;
        }

        private void textBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(e.Key >= Key.A && e.Key <= Key.Z))
            {
                e.Handled = true;
            }
            if (e.Key != Key.Space)
            {
                buttonDown = DateTime.Now;
                int key = KeyInterop.VirtualKeyFromKey(e.Key) - 65;
                ButtonDownTime[key] += timer2.ElapsedMilliseconds;
                ButtonDownCount[key]++;
                timer2.Stop();
                timer2.Reset();
            }     
        }

        private long SumOfArray(long[] array)
        {
            long sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }
            return sum;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.ToString().EndsWith(" "))
            {
                spaceDown = DateTime.Now;
                sapceTime += spaceDown.Subtract(buttonDown);
                spaceCounter++;
                previousWasSpace = true;
            }
            if (textBox.Text.Length == int.Parse(textBoxFirst.Text.ToString())  )
            {
                processKeysValues(0);
            }
            if (textBox.Text.Length == int.Parse(textBoxSecond.Text.ToString()))
            {
                processKeysValues(1);
            }
            if (textBox.Text.Length == int.Parse(textBoxThird.Text.ToString()))
            {
                processKeysValues(2);
                textBox.IsEnabled = false;
                MessageBox.Show("done, tu ma sie opdalać okienko z wynikiem XD");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int a;
            bool value1 = int.TryParse(textBoxFirst.Text.ToString(),out a);
            bool value2 = int.TryParse(textBoxSecond.Text.ToString(), out a);
            bool value3 = int.TryParse(textBoxThird.Text.ToString(), out a);
            if (value1 && value2 && value3)
            {
                textBox.IsEnabled = true;
                textBoxFirst.IsEnabled = false;
                textBoxSecond.IsEnabled = false;
                textBoxThird.IsEnabled = false;
            }
            else
                MessageBox.Show("NaN");
        }
        private void processKeysValues(int threshold)
        {
            timer.Stop();
            long[] MeanButtonsDownTime = new long[26];
            long meanTimeBetweenClicks = timer.ElapsedMilliseconds;
            long[] ButtonDownCount2 = (long[])ButtonDownCount.Clone(); // klonowanie by nie zmieniać globalnych zmiennych
            long meanvalueHelper = 26;

            meanTimeBetweenClicks -= SumOfArray(ButtonDownTime);
            meanTimeBetweenClicks /= (SumOfArray(ButtonDownCount) + spaceCounter);

            for (int i = 0; i < 26; i++)
            {
                if (ButtonDownCount2[i] == 0)
                {
                    ButtonDownCount2[i] = 1;
                    meanvalueHelper--;
                }
                MeanButtonsDownTime[i] = (ButtonDownTime[i] / ButtonDownCount2[i]);
                Console.WriteLine(MeanButtonsDownTime[i]);
                vectors1[threshold][i] = MeanButtonsDownTime[i];
            }

            int buttonSpaceTime = sapceTime.Milliseconds / spaceCounter;
            int spaceButtonTime = sapceTime2.Milliseconds / spaceCounter2;
            long meanButtonDownTime = SumOfArray(MeanButtonsDownTime) / meanvalueHelper;

            Console.WriteLine(meanButtonDownTime);
            Console.WriteLine(meanTimeBetweenClicks);
            Console.WriteLine(buttonSpaceTime);
            Console.WriteLine(spaceButtonTime);
            Console.WriteLine("____________________________________________---------------------------------------_________________________________--");
            vectors2[threshold][0] = meanButtonDownTime;
            vectors2[threshold][1] = meanTimeBetweenClicks;
            vectors2[threshold][2] = buttonSpaceTime;
            vectors2[threshold][3] = spaceButtonTime;
        }
    } 
}
