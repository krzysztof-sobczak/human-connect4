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
using HumanConnect4.Connect4;
using Microsoft.Win32;

namespace Win7Connect4
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NeuralNetwork neuralNetwork;

        public NeuralNetwork NeuralNetwork
        {
            get { return neuralNetwork; }
            set { neuralNetwork = value; }
        }

        TextBoxLogger logger;

        public MainWindow()
        {
            InitializeComponent();
            logger = new TextBoxLogger(Output);
            Console.SetOut(logger);
            Console.WriteLine("Human Connect4 started.");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var xmlFilePath = getXmlLoadFilePath();
            if (xmlFilePath == null)
            {
                Console.WriteLine("No file specifed.");
                return;
            }
            Console.WriteLine("Loading network from \"{0}\" ...", xmlFilePath);
            NeuralNetwork = NeuralNetwork.getNeuralNetworkfromXml(xmlFilePath);
            Console.WriteLine("Finished");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var xmlFilePath = getXmlSaveFilePath();
            if (xmlFilePath == null)
            {
                Console.WriteLine("No file specifed.");
                return;
            }
            Console.WriteLine("Saving network to \"{0}\" ...", xmlFilePath);
            NeuralNetwork.saveNeuralNetworkToXml(xmlFilePath);
            Console.WriteLine("Finished");
        }

        private void TrainButton_Click(object sender, RoutedEventArgs e)
        {
            float learningRate = 0;
            if (!float.TryParse(LearningRate.Text, out learningRate))
            {
                MessageBox.Show("Invalid learning rate value");
                return;
            }
            float momentum = 0;
            if (!float.TryParse(Momentum.Text, out momentum))
            {
                MessageBox.Show("Invalid momentum value");
                return;
            }
            int learningIterations = 0;
            if (!int.TryParse(LearningIterations.Text, out learningIterations))
            {
                MessageBox.Show("Invalid learning iterations value");
                return;
            }
            var csvFilePath = getCsvLoadFilePath();
            if (csvFilePath == null)
            {
                Console.WriteLine("No file specifed.");
                return;
            }
            NeuralNetwork.LEARNING_RATE = learningRate;
            NeuralNetwork.MOMENTUM = momentum;
            NeuralNetwork.TRAIN_ITERATIONS = learningIterations;
            HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet = new HumanConnect4.Connect4.TrainingSets.VelenaCsv(csvFilePath);
            NeuralNetwork.train(trainingSet);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            var csvFilePath = getCsvLoadFilePath();
            if (csvFilePath == null)
            {
                Console.WriteLine("No file specifed.");
                return;
            }
            HumanConnect4.Connect4.TestSets.AbstractTestSet testSet = TestSetFactory.Create<HumanConnect4.Connect4.TestSets.VelenaCsvSeries>();
            NeuralNetwork.test(NeuralNetwork, testSet);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            int randomSeed = 0;
            if (!int.TryParse(RandomSeed.Text, out randomSeed))
            {
                MessageBox.Show("Invalid random seed");
                return;
            }
            HumanConnect4.Random.setSeed(randomSeed);
            NeuralNetwork = new NeuralNetwork(NeuralNetwork.getPartialMovesNetwork());
            Console.WriteLine("Created new network with random seed: {0}",randomSeed);
        }

        private string getCsvLoadFilePath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV data files (*.csv)|*.csv|All Files|*.*";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;
        }

        private string getCsvSaveFilePath()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "CSV data files (*.csv)|*.csv|All Files|*.*";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;
        }

        private string getXmlLoadFilePath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml neural network files (*.xml)|*.xml|All Files|*.*";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;
        }

        private string getXmlSaveFilePath()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Xml neural network files (*.xml)|*.xml|All Files|*.*";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;
        }

    }
}
