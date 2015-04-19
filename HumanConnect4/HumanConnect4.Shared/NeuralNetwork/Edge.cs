using HumanConnect4.NeuralNetwork.Neurons;
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

        private AbstractNeuron input;

        public AbstractNeuron Input
        {
            get { return input; }
            set { input = value; }
        }

        public Edge(float weight, AbstractNeuron input)
        {
            this.weight = weight;
            this.input = input;
        }

        public static void connectAllNeurons(List<Neuron> targetNeurons, List<AbstractNeuron> sourceNeurons)
        {
            foreach(Neuron targetNeuron in targetNeurons)
            {
                foreach(AbstractNeuron sourceNeuron in sourceNeurons)
                {
                    targetNeuron.Edges.Add(new Edge(Random.BipolarFloat(), sourceNeuron));
                }
            }
        }
    }
}
