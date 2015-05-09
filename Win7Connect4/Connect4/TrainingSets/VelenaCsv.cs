using CsvHelper;
using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HumanConnect4.Connect4.TrainingSets
{
    public class VelenaCsv : AbstractTrainingSet
    {

        private const string velenaCsvPath = "velena_learn.csv";

        public VelenaCsv()
        {
            InputLayers = new List<InputLayer>();
            OutputLayers = new List<OutputLayer>();

            AsyncHelpers.RunSync(() => getFromVelenaCsv());
        }

        private async Task getFromVelenaCsv()
        {
            using (var reader = new CsvReader(new StreamReader(@"\Resources\" + velenaCsvPath)))
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
