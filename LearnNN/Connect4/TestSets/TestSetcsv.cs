using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4.TrainingSets
{
    public class TestSetCsv
    {
        public List<string> Contexts { get; set; }
        public List<int> BestColumns { get; set; }

        public InputLayer getInputLayer()
        {
            InputLayer inputLayer = new InputLayer();

            foreach(string context in Contexts)
            {
                ExtendedContext extendedContext = getExtendedContext(context);
                inputLayer.Neurons.AddRange(extendedContext.getPassiveNeurons());
            }

            return inputLayer;
        }

        private ExtendedContext getExtendedContext(string context)
        {
            var splittedContext = context.Replace('{',' ').Replace('}',' ').Split(',');
            List<int> contextValues = new List<int>();
            foreach (string stringValue in splittedContext)
            {
                int value = 0;
                int.TryParse(stringValue, out value);
                contextValues.Add(value);
            }
            return new ExtendedContext(contextValues);
        }
    }

}
