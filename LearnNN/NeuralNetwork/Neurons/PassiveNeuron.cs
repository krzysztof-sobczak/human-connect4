using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Neurons
{
    public class PassiveNeuron : AbstractNeuron
    {
        public PassiveNeuron()
        {

        }

        public PassiveNeuron(int output)
        {
            base.Output = output;
        }

    }
}
