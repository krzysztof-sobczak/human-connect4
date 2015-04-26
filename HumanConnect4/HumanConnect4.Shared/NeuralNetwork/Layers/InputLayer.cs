using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public class InputLayer
    {
        private List<PassiveNeuron> neurons;

        public List<PassiveNeuron> Neurons
        {
            get { return neurons; }
            set { neurons = value; }
        }

        public InputLayer()
        {
            Neurons = new List<PassiveNeuron>();
        }

    }
}
