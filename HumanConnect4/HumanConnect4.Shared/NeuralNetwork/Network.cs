using HumanConnect4.NeuralNetwork.Layers;
using HumanConnect4.NeuralNetwork.Neurons;
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

        public void feedForward(InputLayer inputLayer)
        {
            if (inputLayer.Neurons.Count != InputLayer.Neurons.Count)
            {
                throw new Exception(String.Format("Provided input layer must match number of neurons with current network model: {0} neurons.", InputLayer.Neurons.Count));
            }

            for(int i = 0; i < inputLayer.Neurons.Count; i ++)
            {
                InputLayer.Neurons[i].Output = inputLayer.Neurons[i].Output;
            }

            foreach(AbstractHiddenLayer hiddenLayer in HiddenLayers)
            {
                foreach(Neuron neuron in hiddenLayer.getNeurons())
                {
                    neuron.calculateOutput();
                }
            }

            foreach (Neuron neuron in OutputLayer.Neurons)
            {
                neuron.calculateOutput();
            }
        }

    }
}
