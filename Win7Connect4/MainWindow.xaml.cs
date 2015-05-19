﻿using System;
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
using HumanConnect4.Connect4;

namespace Win7Connect4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AI.getMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AI AI = new AI();
            string globalErrorLog = "";
            foreach(float error in AI.NeuralNetwork.GlobalError)
            {
                globalErrorLog += "global error: " + error.ToString() + "\n";
            }
            textBox.Text = globalErrorLog;

        }
    }
}
