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

	/// <summary>                                                                            
	/// Interaction logic for MainWindow.xaml                                                
	/// </summary>                                                                           
	///                                                                                      
	public partial class MainWindow : Window
	{

		List<Data> database = new List<Data>();
		Dictionary<string, long> dTimes = new Dictionary<string, long>();
		Dictionary<Key, long> fTimes = new Dictionary<Key, long>();
		Stopwatch stopwatchDwell;
		Stopwatch stopwatchFlight;

		//List<long> dwells;
		readonly string textToRewrite = "nosilwilkrazykilkaponiesliiwilka";

		public MainWindow() {
			InitializeComponent();
			setDefaultValues();
			database = loadDatabase();
		}

		private List<Data> loadDatabase() {

			List<Data> db = new List<Data>();
			string[] files = { "marek_dwell.txt", "michal_dwell.txt", "test_dwell.txt" };

			foreach (var file in files) {
				dTimes = new Dictionary<string, long>();
				string[] lines = File.ReadAllLines(file);
				string username = file.Remove(file.Length - 10); // nazwa pliku - "_dwell.txt" (10 znaków)
				foreach (var line in lines) {
					string letter = line.Substring(0,1);
					int time = Int32.Parse(line.Substring(2));
					dTimes.Add(letter, time);
				}
				db.Add(new Data(username, dTimes, fTimes));
			}

			return db;
		}

		

		char[,] quantityOfEachLetter = { { 'A', '1' }, { 'B', '1' }, { 'C', '1' }, { 'D', '1' }, { 'E', '1' }, { 'F', '1' }, { 'G', '1' }, { 'H', '1' }, { 'I', '1' }, { 'J', '1' },
			{ 'K', '1' }, { 'L', '1' }, { 'M', '1' }, { 'N', '1' }, { 'O', '1' }, { 'P', '1' }, { 'Q', '1' }, { 'R', '1' }, { 'S', '1' }, { 'T', '1' }, { 'U', '1' },
			{ 'V', '1' }, { 'W', '1' }, { 'X', '1' }, { 'Y', '1' }, { 'Z', '1' } };


		//List<long> lista = new List<long>();

		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			stopwatchFlight.Stop();
		   
			/*if (!FTime.TryAdd(e.Key, stopwatchFlight.ElapsedMilliseconds))
			{
				KeyConverter kc = new KeyConverter();
				string keyChar = kc.ConvertToString(e.Key);

				for (int i = 0; i < 26; i++)
				{
					if ((quantity[i, 0].ToString()).Equals(keyChar.ToString())) //to do 
					{
						FTime[e.Key] *= (quantity[i, 1]);
						FTime[e.Key] += stopwatchFlight.ElapsedMilliseconds;
						FTime[e.Key] /= (quantity[i, 1]);
					}

				}

			}*/
						
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
			//lista.Add(time);
			if (!dTimes.TryAdd(letter, dwellTime))
			{
				//KeyConverter kc = new KeyConverter();
				//string keyChar = kc.ConvertToString(e.Key);
				string keyChar = e.Key.ToString();

				for (int i = 0; i < 26; i++)
				{
					if ((quantityOfEachLetter[i, 0].ToString()).Equals(keyChar.ToString()))
					{
					 
						
						dTimes[letter] *= Convert.ToInt32(new string((quantityOfEachLetter[i, 1]), 1));
						dTimes[letter] += dwellTime;//stopwatchDwell.ElapsedMilliseconds;
						dTimes[letter] /= Convert.ToInt32(new string((++quantityOfEachLetter[i, 1]), 1));
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

			database.Add(new Data(name_textblock.Text, dTimes, fTimes));
			using TextWriter twDwell = new StreamWriter(name_textblock.Text + "_dwell.txt");
			using TextWriter twFlight = new StreamWriter(name_textblock.Text + "_flight.txt");

			foreach (KeyValuePair<string, long> x in dTimes)
			{
				twDwell.WriteLine("{0} {1}", x.Key, x.Value);

			}
			foreach (KeyValuePair<Key, long> y in fTimes)
			{
				twFlight.WriteLine("{0} {1}", y.Key, y.Value);
			}
			setDefaultValues();

		}

		public void KNN()
		{
		   
		}

		private void recognize_button_Click(object sender, RoutedEventArgs e) {

			setDefaultValues();

			int distance;

			//manhattan
			List<Distance> distancesManhattan = new List<Distance>();
			for (int i = 0; i < database.Count -1; i++) {
				distance = Metrics.MetricManhattan(dTimes, database[i].dwellTimes);
				Distance dist = new Distance();
				dist.name = database[i].Person;
				dist.value = distance;
				distancesManhattan.Add(dist);
			}
			distancesManhattan.Sort((p, q) => p.value.CompareTo(q.value)); // sortowanie po value
			
			MessageBox.Show(distancesManhattan.ElementAt(0).name);

		}
	}
}
