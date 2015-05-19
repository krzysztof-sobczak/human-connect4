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
        const int FILES_LIMIT = 3;

        public VelenaCsvSeries()
        {
            InputLayers = new List<InputLayer>();
            OutputLayers = new List<OutputLayer>();

            AsyncHelpers.RunSync(() => getFromVelenaCsv());
        }

        private async Task getFromVelenaCsv()
        {

            DirectoryInfo dirInfo = new DirectoryInfo(@"D:\dawid\studia\msi2\repo\LearnNN\Resources\test");

            FileInfo[] info = dirInfo.GetFiles("*.*");
            int counter = 0;
            foreach (FileInfo f in info)
            {
                counter++;
                if (counter >= FILES_LIMIT)
                {
                    break;
                }
                using (var reader = new CsvReader(new StreamReader(f.FullName)))
                {
                    reader.Configuration.RegisterClassMap<TrainingSetCsvMap>();
                    while (reader.Read())
                    {
                        TrainingSetCsv instance = reader.GetRecord<TrainingSetCsv>();
                        InputLayers.Add(instance.getInputLayer());
                        OutputLayers.Add(columnNumberToOutputLayer(instance.BestColumn));
                    }
                }
            }
        }
        
    }
}
