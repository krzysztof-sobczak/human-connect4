using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public class OutputLayer : Layer
    {
        public OutputLayer(int size) : base(size) { }
        public OutputLayer(List<float> values)
        {
            foreach(float value in values)
            {
                base.Neurons.Add(new Neuron(value));
            }
        }
    }
}
