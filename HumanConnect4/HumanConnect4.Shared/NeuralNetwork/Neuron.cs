using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    class Neuron
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
        private float output;

        public float Output
        {
            get { return output; }
            set { output = value; }
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
    }
}
