using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Neurons
{
    class Neuron : AbstractNeuron
    {
        private int bias = 1;

        public int Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        private float delta;

        public float Delta
        {
            get { return delta; }
            set { delta = value; }
        }


        private float error;

        public float Error
        {
            get { return error; }
            set { error = value; }
        }

        private List<Edge> edges;

        public List<Edge> Edges
        {
            get { return edges; }
            set { edges = value; }
        }

        public Neuron()
        {
            this.Edges = new List<Edge>();
        }

        public void calculateOutput()
        {
            float output = 0;
            foreach(Edge edge in Edges)
            {
                output += Bias + edge.Input.Output * edge.Weight;
            }
            this.Output = ActivationFunction.sigmoid(output);
        }
    }
}
