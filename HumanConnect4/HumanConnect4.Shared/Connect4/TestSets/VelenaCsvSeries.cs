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
    public class VelenaCsvSeries : AbstractTrainingSet
    {
        public VelenaCsvSeries()
        {
            InputLayers = new List<InputLayer>();
            OutputLayers = new List<OutputLayer>();

            AsyncHelpers.RunSync(() => getFromVelenaCsv());
        }

        private async Task getFromVelenaCsv()
        {
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Resources\learn");
            List<StorageFile> files = new List<StorageFile>(await folder.GetFilesAsync()).GetRange(0,4);
            foreach (StorageFile file in files)
            {
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
}
