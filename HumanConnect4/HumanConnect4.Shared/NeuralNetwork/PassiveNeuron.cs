using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    class PassiveNeuron
    {
        private float output;

        public float Output
        {
            get { return output; }
        }

        public PassiveNeuron(int output)
        {
            this.output = output;
        }

    }
}
