using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    public abstract class AbstractTestSet
    {
        private List<InputLayer> inputLayers;

        public List<InputLayer> InputLayers
        {
            get { return inputLayers; }
            set { inputLayers = value; }
        }

        private List<OutputLayer> outputLayers;

        public List<OutputLayer> OutputLayers
        {
            get { return outputLayers; }
            set { outputLayers = value; }
        }
    }
}
