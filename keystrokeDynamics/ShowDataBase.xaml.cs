﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace keystrokeDynamics
{
    /// <summary>
    /// Logika interakcji dla klasy ShowDataBase.xaml
    /// </summary>
    public partial class ShowDataBase : Window
    {
        public string[] files;
        public string[] filesFilght;
        public ShowDataBase(string[] showFiles)
        {
            InitializeComponent();
            files = showFiles;
            CreateTable();
        }
        public void CreateTable()
        {
            string[,] data = new string[5, 100];
            string[,] dataFilght = new string[5, 100];
            int numberFile = 0;
            string showDataString = "";
            try
            {
                file1.Content = files[0];
                file2.Content = files[1];
            }
            catch { }
            foreach (var file in files)
            {

                string[] lines = File.ReadAllLines(file);

                int count = 0;

                foreach (var line in lines)
                {
                    showDataString = showDataString + line + "\n";

                }
                filesFilght = new string[files.Length];
                count = 0;
                foreach (var item in files)
                {
                    filesFilght[count] = item.Remove(item.Length - 10, 10);
                    filesFilght[count] = filesFilght[count] + "_flight.txt";
                    count++;
                }
                if (numberFile == 0) data1.Content = showDataString;
                else data2.Content = showDataString;
                numberFile++;
                showDataString = "";

            }
            numberFile = 0;
            showDataString = "";
            try
            {
                file1_1.Content = filesFilght[0];
                file2_2.Content = filesFilght[1];
            }
            catch { }
            foreach (var file in filesFilght)
            {
                string[] lines = File.ReadAllLines(file);
                int count = 0;

                foreach (var line in lines)
                {
                    count++;
                    if (count % 2 == 0) showDataString = showDataString + "  -   " + line + "\n";
                    else showDataString = showDataString + line;

                }
                if (numberFile == 0) data1_1.Content = showDataString;
                else data2_2.Content = showDataString;
                numberFile++;
                showDataString = "";
            }
        }

    }
}
