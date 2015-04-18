using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    class Network
    {
        private InputLayer inputLayer;

        public InputLayer InputLayer
        {
            get { return inputLayer; }
            set { inputLayer = value; }
        }
        

        private List<AbstractHiddenLayer> hiddenLayers;
        public List<AbstractHiddenLayer> HiddenLayers
        {
            get { return hiddenLayers; }
            set { hiddenLayers = value; }
        }

        private OutputLayer outputLayer;
        public OutputLayer OutputLayer
        {
            get { return outputLayer; }
            set { outputLayer = value; }
        }

        public Network()
        {
            HiddenLayers = new List<AbstractHiddenLayer>();
        }

    }
}
