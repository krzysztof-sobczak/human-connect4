using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    class Edge
    {
        private float weight;

        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private Neuron input;

        public Neuron Input
        {
            get { return input; }
            set { input = value; }
        }

        public Edge(float weight, Neuron input)
        {
            this.weight = weight;
            this.input = input;
        }
    }
}
