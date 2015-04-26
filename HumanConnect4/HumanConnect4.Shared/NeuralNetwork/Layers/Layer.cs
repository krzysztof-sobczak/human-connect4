using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public class Layer
    {
        private List<Neuron> neurons;

        public List<Neuron> Neurons
        {
            get { return neurons; }
            set { neurons = value; }
        }

        public  Layer()
        {
            this.Neurons = new List<Neuron>();
        }

        public Layer(List<Neuron> neurons)
        {
            this.Neurons = neurons;
        }

        public Layer(int size)
        {
            this.Neurons = new List<Neuron>();
            for(int i = 0; i < size; i ++)
            {
                this.Neurons.Add(new Neuron());
            }
        }

        public Layer Clone()
        {
            List<Neuron> neurons = new List<Neuron>();
            foreach(Neuron neuron in this.Neurons)
            {
                neurons.Add(new Neuron());
            }
            return new Layer(neurons);
        }

    }
}
