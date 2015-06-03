using CsvHelper;
using HumanConnect4.Connect4.TrainingSets;
using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HumanConnect4.Connect4.TestSets
{
    public class VelenaCsvSeries : AbstractTestSet
    {
        const int FILES_LIMIT = 999;

        public VelenaCsvSeries()
        {
            InputLayers = new List<InputLayer>();
            OutputLayers = new List<OutputLayer>();
            expectedColumns = new List<List<int>>();

            AsyncHelpers.RunSync(() => getFromVelenaCsv());
        }

        private async Task getFromVelenaCsv()
        {
            String path = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase
            ).Substring(6);
            DirectoryInfo dirInfo = new DirectoryInfo(path + @"\test");

            FileInfo[] info = dirInfo.GetFiles("*.*");
            int counter = 0;
            foreach (FileInfo f in info)
            {
                counter++;
                if (counter >= FILES_LIMIT)
                {
                    break;
                }
                Console.WriteLine("Loading " + f.FullName + "...");
                using (var reader = new CsvReader(new StreamReader(f.FullName)))
                {
                    reader.Configuration.RegisterClassMap<TestSetCsvMap>();
                    while (reader.Read())
                    {
                        TestSetCsv instance = reader.GetRecord<TestSetCsv>();
                        InputLayers.Add(instance.getInputLayer());
                        expectedColumns.Add(instance.BestColumns);
                    }
                }
            }
        }
        
    }
}
