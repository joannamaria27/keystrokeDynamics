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
using System.Text.RegularExpressions;

namespace keystrokeDynamics
{

    /// <summary>                                                                            
    /// Interaction logic for MainWindow.xaml                                                
    /// </summary>                                                                           
    ///                                                                                      
    public partial class MainWindow : Window
    {

        List<Data> database = new List<Data>();
        Dictionary<string, long> dTimes = new Dictionary<string, long>();
        Dictionary<string, long> fTimes = new Dictionary<string, long>();
        Stopwatch stopwatchDwell;
        Stopwatch stopwatchFlight;

        readonly string textToRewrite = "nosilwilkrazykilkaponiesliiwilka";

        public MainWindow()
        {
            InitializeComponent();
            setDefaultValues();
            database = loadDatabase();
        }

        private List<Data> loadDatabase()
        {
            List<Data> db = new List<Data>();
            string[] files = { "test_dwell.txt", "asia4_dwell.txt", "asia3_dwell.txt" }; //ustawić swoje

            foreach (var file in files)
            {
                dTimes = new Dictionary<string, long>();
                string[] lines = File.ReadAllLines(file);
                string username = file.Remove(file.Length - 10); // nazwa pliku - "_dwell.txt" (10 znaków)
                foreach (var line in lines)
                {
                    string letter = line.Substring(0, 1);
                    int time = Int32.Parse(line.Substring(2));
                    dTimes.Add(letter, time);
                }
                db.Add(new Data(username, dTimes, fTimes));
            }

            return db;
        }

        char[,] quantityOfEachLetter = { { 'A', '1' }, { 'B', '1' }, { 'C', '1' }, { 'D', '1' }, { 'E', '1' }, { 'F', '1' }, { 'G', '1' }, { 'H', '1' }, { 'I', '1' }, { 'J', '1' }, { 'K', '1' }, { 'L', '1' }, { 'M', '1' }, { 'N', '1' }, { 'O', '1' }, { 'P', '1' }, { 'Q', '1' }, { 'R', '1' }, { 'S', '1' }, { 'T', '1' }, { 'U', '1' }, { 'V', '1' }, { 'W', '1' }, { 'X', '1' }, { 'Y', '1' }, { 'Z', '1' } };

        string[,] quantityOfLetter = { { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" },
         { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" }, { "0", "0" },};//new string[676, 2]; //to do

        List<long> lista = new List<long>();
        string keyCharFirst = "-";
        long flightTime = 0;
        int l = 0;
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            stopwatchFlight.Stop();
            flightTime = stopwatchFlight.ElapsedMilliseconds;
            KeyConverter kc = new KeyConverter();
            string keyChar = keyCharFirst + kc.ConvertToString(e.Key);

            lista.Add(flightTime);
            if (!fTimes.TryAdd(keyChar, flightTime))
            {
                bool z = false;
                for (int i = 0; i < quantityOfLetter.Length / 2; i++)
                {
                    if ((quantityOfLetter[i, 0]).Equals(keyChar.ToString()))
                    {
                        int quantity = (Convert.ToInt32(quantityOfLetter[i, 1]));
                        if (quantity > 9)
                            z = true;
                        else
                        {
                            fTimes[keyChar] *= quantity;
                            fTimes[keyChar] += flightTime;
                            fTimes[keyChar] /= ++quantity;
                            quantityOfLetter[i, 1] = quantity.ToString();
                            z = true;
                        }
                    }
                }
                if (z == false)
                {
                    quantityOfLetter[l, 0] = keyChar;
                    quantityOfLetter[l++, 1] = "2";
                    fTimes[keyChar] += flightTime;
                    fTimes[keyChar] /= 2;
                }
            }
            keyCharFirst = kc.ConvertToString(e.Key);
            stopwatchDwell = new Stopwatch();
            stopwatchDwell.Start();
            stopwatchFlight = new Stopwatch();
            stopwatchFlight.Start();
        }

        private void inputBox_KeyUp(object sender, KeyEventArgs e)
        {
            string letter = e.Key.ToString();
            stopwatchDwell.Stop();
            var dwellTime = stopwatchDwell.ElapsedMilliseconds;

            if (!dTimes.TryAdd(letter, dwellTime))
            {
                string keyChar = e.Key.ToString();
                for (int i = 0; i < 26; i++)
                {
                    if ((quantityOfEachLetter[i, 0].ToString()).Equals(keyChar.ToString()))
                    {
                        int quantity = Convert.ToInt32(new string((quantityOfEachLetter[i, 1]), 1));
                        if (quantity > 9)
                            break;
                        else
                        {
                            dTimes[letter] *= quantity;
                            dTimes[letter] += dwellTime;
                            dTimes[letter] /= ++quantity; //
                        }
                    }
                }
            }
            //textInfo.Content = e.Key + " " + stopwatchDawell.ElapsedMilliseconds + "ms";
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            textToRewriteLabel.Content = textToRewrite;
            inputBox.Text = "";
            stopwatchFlight = new Stopwatch();
            dTimes.Clear();
            fTimes.Clear();

            for (int i = 0; i < quantityOfLetter.Length / 2; i++)
                for (int j = 0; j < 2; j++)
                    quantityOfLetter[i, j] = "0";

            for (int i = 0; i < quantityOfEachLetter.Length / 2; i++)
                quantityOfEachLetter[i, 1] = '1';

            keyCharFirst = "-";
            flightTime = 0;
            l = 0;
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            database.Add(new Data(name_textblock.Text, dTimes, fTimes));
            using TextWriter twDwell = new StreamWriter(name_textblock.Text + "_dwell.txt");
            using TextWriter twFlight = new StreamWriter(name_textblock.Text + "_flight.txt");

            foreach (KeyValuePair<string, long> x in dTimes)
            {
                twDwell.WriteLine("{0} {1}", x.Key, x.Value);

            }
            foreach (KeyValuePair<string, long> y in fTimes)
            {
                twFlight.WriteLine("{0} {1}", y.Key, y.Value);
            }
            setDefaultValues();
        }

        public void KNN()
        {
            int k = Int32.Parse(k_textbox.Text.ToString());
            int distance;
            List<Distance> distances = new List<Distance>();

            for (int i = 0; i < database.Count - 1; i++)
            {
                if (manhattan_combobox.IsSelected)
                {
                    distance = Metrics.MetricManhattan(dTimes, database[i].dwellTimes);
                }
                else if (euklides_combobox.IsSelected)
                {
                    distance = Metrics.MetricEuklides(dTimes, database[i].dwellTimes);
                }
                else if (czebyszew_combobox.IsSelected)
                {
                    distance = Metrics.MetricCzebyszew(dTimes, database[i].dwellTimes);
                }
                else
                {
                    MessageBox.Show("No selected option");
                    return;
                }

                Distance dist = new Distance();
                dist.name = database[i].Person;
                dist.value = distance;
                distances.Add(dist);
            }
            distances.Sort((p, q) => p.value.CompareTo(q.value)); // sortowanie po value
            MessageBox.Show(distances.ElementAt(0).name);
        }

        private void recognize_button_Click(object sender, RoutedEventArgs e)
        {
            KNN();
            setDefaultValues();
        }

        private void k_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
