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
        public MainWindow()
        {
            InitializeComponent();
            timer = new Stopwatch();
            timer2 = new Stopwatch();
            ButtonDownTime = new long[26];
            ButtonDownCount = new long[26];
            MeanButtonDownTime = new long[26];
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            DBConnect dbconnector = new DBConnect();
            DataTable lista = dbconnector.SelectAll();


            string elo = lista.Rows[3].ItemArray[2].ToString();// pobiera input 1
            string[] splittedstring = elo.Split(' ');
            List<string[]> splied = new List<string[]>();
            foreach (var item in splittedstring)
            {
                splied.Add(item.Split('_'));
            }
            //foreach (var keyEvent in splied)
            //{
            //    foreach (var item2 in collection)
            //    {

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
            else if (e.Key == Key.Space)
            {

            }         
        }

        private void textBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space))
            {
                e.Handled = true;
            }
            else if ((e.Key != Key.Space))
            {
                int key = KeyInterop.VirtualKeyFromKey(e.Key) - 65;
                ButtonDownTime[key] += timer2.ElapsedMilliseconds;
                ButtonDownCount[key]++;
                timer2.Stop();
                timer2.Reset();
            }
            else if (e.Key == Key.Space)
            {

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            long meanTimeBetweenClicks = timer.ElapsedMilliseconds;
            meanTimeBetweenClicks -= SumOfArray(ButtonDownTime); /// nalezy odjąć jeszcze czas spacji
            meanTimeBetweenClicks /= ( SumOfArray(ButtonDownCount) -1);
            for (int i = 0; i < 26; i++)
            {
                if (ButtonDownCount[i] == 0)
                    ButtonDownCount[i] = 1;
                MeanButtonDownTime[i] = (ButtonDownTime[i] / ButtonDownCount[i]);
                Console.WriteLine(MeanButtonDownTime[i]);
            }
            Console.WriteLine(meanTimeBetweenClicks);
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
    } 
}
