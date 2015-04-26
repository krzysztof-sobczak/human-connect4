using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    abstract class AbstractHiddenLayer
    {
        abstract public List<Neuron> getNeurons();

        abstract public void calculateDeltas();
    }
}
