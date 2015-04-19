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

        public void calculateDelta()
        {
            Delta = Error * ActivationFunction.sigmoidDerivative(Output);
        }
    }
}
