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
        Stopwatch timer;
        Stopwatch timer2;
        public long[] MeanButtonDownTime;
        public long[] ButtonDownTime;
        public long[] ButtonDownCount;
        DateTime buttonDown;
        DateTime spaceDown;
        TimeSpan sapceTime;
        TimeSpan sapceTime2;
        bool previousWasSpace;
        int spaceCounter;
        int spaceCounter2;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Stopwatch();
            timer2 = new Stopwatch();
            ButtonDownTime = new long[26];
            ButtonDownCount = new long[26];
            MeanButtonDownTime = new long[26];
            sapceTime = new TimeSpan();
            sapceTime2 = new TimeSpan();
            previousWasSpace = false;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DBConnect dbconnector = new DBConnect();
            DataTable lista = dbconnector.SelectAll();


            string elo = lista.Rows[3].ItemArray[3].ToString();// pobiera input 1
            string[] splittedstring = elo.Split(' ');
            List<string[]> splied = new List<string[]>();
            foreach (var item in splittedstring)
            {
                splied.Add(item.Split('_'));
            }
            //foreach (string[] keyEvent in splied)
            //{
            //    foreach (var item in keyEvent)
            //    {
            //        if(item[0] == 'd')
            //        {

            //        }
            //    }
            //}
            dataGrid.AutoGenerateColumns = true;
            dataGrid.ItemsSource = lista.DefaultView;
            MessageBox.Show("done");
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            long meanTimeBetweenClicks = timer.ElapsedMilliseconds;
            meanTimeBetweenClicks -= SumOfArray(ButtonDownTime); /// nalezy odjąć jeszcze czas spacji
            meanTimeBetweenClicks /= ( SumOfArray(ButtonDownCount) + spaceCounter);
            for (int i = 0; i < 26; i++)
            {
                if (ButtonDownCount[i] == 0)
                    ButtonDownCount[i] = 1;
                MeanButtonDownTime[i] = (ButtonDownTime[i] / ButtonDownCount[i]);
                Console.WriteLine(MeanButtonDownTime[i]);
            }
            int output = sapceTime.Milliseconds / spaceCounter;
            int output2 = sapceTime2.Milliseconds / spaceCounter2;

            //Console.WriteLine(SumOfArray(MeanButtonDownTime) / SumOfArray(ButtonDownCount));
            Console.WriteLine(meanTimeBetweenClicks);
            Console.WriteLine(output);
            Console.WriteLine(output2);
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
        }
    } 
}
