using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public abstract class AbstractHiddenLayer
    {
        private bool isFrozen = false;

        public bool IsFrozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }


        abstract public List<Neuron> getNeurons();

        abstract public void calculateDeltas();

        abstract public void calculateOutput();
    }
}
