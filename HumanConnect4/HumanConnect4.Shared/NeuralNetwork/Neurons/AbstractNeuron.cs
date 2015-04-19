using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Neurons
{
    abstract class AbstractNeuron
    {
        private float output;

        public float Output
        {
            get { return output; }
            set { output = value; }
        }
    }
}
