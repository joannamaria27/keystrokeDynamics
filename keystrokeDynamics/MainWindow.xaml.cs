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
using System.Diagnostics;
using System.IO;

namespace keystrokeDynamics
{

    public class Data // nie wiem czy tak robimy czy inaczej; ale zostawiam
    {
        //public int DataId { get; private set; }
        public string Person { get; private set; }
        public Key Key { get; private set; }
        public long DawellTime { get; private set; }
        public long FlightTime { get; private set; }

        public Data(string _Person, Key _Key, long _DawellTime, long _FlightTime)
        {
            //  DataId = _DataId;
            Person = _Person;
            Key = _Key;
            DawellTime = _DawellTime;
            FlightTime = _FlightTime;
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        List<Data> DataList = new List<Data>();

        Stopwatch stopwatchDawell;
        Stopwatch stopwatchFlight;

        //List<long> dwells;
        readonly string textToRewrite = "nosil wilk razy kilka poniesli i wilka";

        #region Metric
        private double MetricEuklides(long[] X, long[] Y)
        {
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += (X[i] - Y[i]) * (X[i] - Y[i]);
            }
            double distance = Math.Sqrt(sum);
            return distance;
        }
        private double MetricManhattan(long[] X, long[] Y)
        {
            double distance = 0;
            for (int i = 0; i < X.Length; i++)
            {
                distance += Math.Abs(X[i] - Y[i]);
            }
            return distance;
        }
        private double MetricCzebyszew(long[] X, long[] Y)
        {
            double[] sum = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                sum[i] = (X[i] - Y[i]);
            }
            List<double> sumList = new List<double>(sum);
            sumList.Sort();
            double distance = sumList[0];
            return distance;
        }
        private double MetricMahalanobisa(long[] X, long[] Y) //todo
        {
            double sum = 0;

            double distance = Math.Sqrt(sum);

            return distance;
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            setDefaultValues();
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            stopwatchDawell = new Stopwatch();
            stopwatchDawell.Start();
        }
        long time = 0;

        private void inputBox_KeyUp(object sender, KeyEventArgs e)
        {

            stopwatchFlight.Stop();
            time = stopwatchFlight.ElapsedMilliseconds;
            stopwatchDawell.Stop();
            stopwatchFlight = new Stopwatch();
            stopwatchFlight.Start();
            // dwells.Add(stopwatch.ElapsedMilliseconds);
            DataList.Add(new Data(name_textblock.Name, e.Key, stopwatchDawell.ElapsedMilliseconds, time));

            textInfo.Content = e.Key + " " + stopwatchDawell.ElapsedMilliseconds + "ms";
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            textToRewriteLabel.Content = textToRewrite;
            //dwells = new List<long>();
            DataList = new List<Data>();
            inputBox.Text = "";
            stopwatchFlight = new Stopwatch();
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            /* string dwellsAsString = "";
             foreach (long d in dwells)
             {
                 dwellsAsString += d + " ";
             }
             File.WriteAllText(name_textblock.Text + ".txt", dwellsAsString);*/

            using TextWriter tw = new StreamWriter(name_textblock.Text + ".txt");
            foreach (Data s in DataList)
                tw.WriteLine("KEY: " + s.Key + " DAWELL TIME: " + s.DawellTime + " FLIGHT TIME: " + s.FlightTime);


        }

        public void KNN()
        {
            long[,] tableValueDawell = new long[textToRewrite.Length, textToRewrite.Length];
            //todo - okreslanie k 
            int k = 3;
            int i = 0;
            foreach (Data s in DataList)
            {
                tableValueDawell[i++,0] = s.DawellTime;
            }




        }


    }
}
