using CsvHelper;
using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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
            IReadOnlyList<StorageFolder> folders = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFoldersAsync();
            List<StorageFolder> list = new List<StorageFolder>(folders);
            List<StorageFile> files = new List<StorageFile>(await list[1].GetFilesAsync());
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Resources/" + velenaCsvPath));
            using (Stream stream = (await file.OpenReadAsync()).AsStreamForRead())
            {
                using (var reader = new CsvReader(new StreamReader(stream)))
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
