using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Neurons
{
    public class Neuron : AbstractNeuron
    {
        private int bias = 1;

        public int Bias
        {
            get { return bias; }
            set { bias = value; }
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

        public Neuron(float value)
        {
            this.Edges = new List<Edge>();
            this.Output = value;
        }

        public void calculateOutput()
        {
            float output = Bias;
            foreach (Edge edge in Edges)
            {
                // optimize iterations and prepare during feedForward for further backpropagation
                // so clear Error and Delta
                edge.Input.Error = 0;
                edge.Input.Delta = 0;
                output += edge.Input.Output * edge.Weight;
            }
            this.Output = ActivationFunction.sigmoid(output);
            //System.Diagnostics.Debug.WriteLine(String.Format("Calculated output: {0}", this.Output));
        }
    }
}
