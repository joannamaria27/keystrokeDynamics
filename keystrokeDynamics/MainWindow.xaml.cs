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

namespace keystrokeDynamics {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		Stopwatch stopwatch;
		List<long> dwells;
		string textToRewrite = "nosil wilk razy kilka poniesli i wilka";

		public MainWindow() {
			InitializeComponent();
			setDefaultValues();
		}

		

		private void inputBox_KeyDown(object sender, KeyEventArgs e) {
			stopwatch = new Stopwatch();
			stopwatch.Start();
		}

		private void inputBox_KeyUp(object sender, KeyEventArgs e) {

			stopwatch.Stop();
			dwells.Add(stopwatch.ElapsedMilliseconds);
			long measuredTime = stopwatch.ElapsedMilliseconds;

			textToRewriteLabel.Content = e.Key + " " + measuredTime + "ms";
		}

		private void reset_button_Click(object sender, RoutedEventArgs e) {
			setDefaultValues();
		}

		private void setDefaultValues() {
			textToRewriteLabel.Content = textToRewrite;
			dwells = new List<long>();
			inputBox.Text = "";
		}

		private void save_button_Click(object sender, RoutedEventArgs e) {
			string dwellsAsString = "";
			foreach(long d in dwells) {
				dwellsAsString += d + " ";
			}
			File.WriteAllText(name_textblock.Text + ".txt", dwellsAsString);

		}
	}
}
