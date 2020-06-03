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

    public class Data
    {
        //public int DataId { get; private set; }                                                                  
        public string Person { get; private set; }

        Dictionary<Key, long> DwellTime;
        Dictionary<Key, long> FlightTime;

        public Data(string _Person, Dictionary<Key, long> _DwellTime, Dictionary<Key, long> _FlightTime)
        {
            //  DataId = _DataId;                                                            
            Person = _Person;
            DwellTime = _DwellTime;
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

        Stopwatch stopwatchDwell;
        Stopwatch stopwatchFlight;

        //List<long> dwells;
        readonly string textToRewrite = "nosilwilkrazykilkaponiesliiwilka";

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

        char[,] quantity = { { 'A', '1' }, { 'B', '1' }, { 'C', '1' }, { 'D', '1' }, { 'E', '1' }, { 'F', '1' }, { 'G', '1' }, { 'H', '1' }, { 'I', '1' }, { 'J', '1' },
            { 'K', '1' }, { 'L', '1' }, { 'M', '1' }, { 'N', '1' }, { 'O', '1' }, { 'P', '1' }, { 'Q', '1' }, { 'R', '1' }, { 'S', '1' }, { 'T', '1' }, { 'U', '1' },
            { 'W', '1' }, { 'V', '1' }, { 'X', '1' }, { 'Y', '1' }, { 'Z', '1' } };


        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            stopwatchFlight.Stop();

            if (!FTime.TryAdd(e.Key, stopwatchFlight.ElapsedMilliseconds))
            {
                KeyConverter kc = new KeyConverter();
                string keyChar = kc.ConvertToString(e.Key);

                for (int i = 0; i < 26; i++)
                {
                    if ((quantity[i, 0]).Equals(keyChar)) //to do 
                    {
                        FTime[e.Key] += stopwatchFlight.ElapsedMilliseconds;
                        FTime[e.Key] /= ++(quantity[i, 1]); //to do
                    }

                }

            }
                        
            stopwatchDwell = new Stopwatch();
            stopwatchDwell.Start();
            stopwatchFlight = new Stopwatch();
            stopwatchFlight.Start();
        }

        Dictionary<Key, long> DTime = new Dictionary<Key, long>();
        Dictionary<Key, long> FTime = new Dictionary<Key, long>();
        private void inputBox_KeyUp(object sender, KeyEventArgs e)
        {
            stopwatchDwell.Stop();
            DTime.Add(e.Key, stopwatchDwell.ElapsedMilliseconds);
 
            //textInfo.Content = e.Key + " " + stopwatchDawell.ElapsedMilliseconds + "ms";
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            textToRewriteLabel.Content = textToRewrite;
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

            //using TextWriter tw = new StreamWriter(name_textblock.Text + ".txt");
            //foreach (Data s in DataList)
            //    tw.WriteLine( s.Person );

            DataList.Add(new Data(name_textblock.Text, DTime, FTime));
            using TextWriter tw = new StreamWriter(name_textblock.Text + ".txt");

            foreach (KeyValuePair<Key, long> x in DTime)
            {
                tw.WriteLine("Key = {0}, Value Dwell = {1}", x.Key, x.Value);

            }
            foreach (KeyValuePair<Key, long> y in FTime)
            {
                tw.WriteLine("Key = {0}, Value Flight  = {1}", y.Key, y.Value);
            }

        }

        public void KNN()
        {
           
        }


    }
}
