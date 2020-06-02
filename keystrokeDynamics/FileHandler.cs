using System;
using System.Collections.Generic;
using System.Text;

namespace keystrokeDynamics {

	// zapis i odczyt czasu nacisniecia kazdego znaku
	class FileHandler {
		public static void saveListToFile(int[] arr, string path) {

			string text = "";
			for (int i = 0; i < arr.Length; i++) {
				text += arr[i] + " ";
			}

			System.IO.File.WriteAllText(path + ".txt", text);
		}

		// odczyt czasów nacisniecia kazdego znaku z pliku
		public static void readListFromFile() {

		}
	}
}
