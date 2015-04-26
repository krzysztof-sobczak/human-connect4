using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4.TrainingSets
{
    class Csv : AbstractTrainingSet
    {

        public Sample()
        {
            InputLayers = new List<InputLayer>();
            InputLayers.Add(getInputLayer());

            OutputLayers = new List<OutputLayer>();
            OutputLayers.Add(columnNumberToOutputLayer(2));
        }

        private InputLayer getInputLayer()
        {
            InputLayer inputLayer = new InputLayer();

            for (int i = 0; i < NUMBER_OF_CONTEXTS; i++)
            {
                ExtendedContext context = ExtendedContext.getSample();
                inputLayer.Neurons.AddRange(context.getPassiveNeurons());
            }

            return inputLayer;
        }
        
    }
}
