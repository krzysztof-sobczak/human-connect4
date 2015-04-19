using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    class Layer
    {
        private List<Neuron> neurons;

        public List<Neuron> Neurons
        {
            get { return neurons; }
            set { neurons = value; }
        }

        public Layer()
        {
            this.Neurons = new List<Neuron>();
        }

        public Layer(int size)
        {
            this.Neurons = new List<Neuron>();
            for(int i = 0; i < size; i ++)
            {
                this.Neurons.Add(new Neuron());
            }
        }
    }
}
