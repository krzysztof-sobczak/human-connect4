using HumanConnect4.NeuralNetwork;
using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4
{
    public class ExtendedContext
    {
        private const int BIAS = 1;
        private const int FEATURES_LENGTH = 5;
        private const int ATTRIBUTES_LENGTH = 5;
        private const int VALUES_LENGTH = FEATURES_LENGTH + ATTRIBUTES_LENGTH;
        public const int CONTEXT_LENGTH = VALUES_LENGTH + 1;

        private List<int> values;

	    public List<int> Values
	    {
		    get { return values;}
		    set { values = value;}
	    }

        private int bias;

        public ExtendedContext (List<int> values, int? bias = null)
        {
            if(values.Count == VALUES_LENGTH) {
                if(bias != null) {
                    this.bias = int.Parse(bias.ToString());
                }
                else
                {
                    this.bias = BIAS;
                }
                values.Add(this.bias);
                this.Values = values;
            } else {
                throw new Exception(String.Format("Context 'values' parameter accepts only list with {0} values", VALUES_LENGTH));
            }
        }

        public List<PassiveNeuron> getPassiveNeurons()
        {
            List<PassiveNeuron> passiveNeurons = new List<PassiveNeuron>();
            foreach (int value in this.Values)
            {
                PassiveNeuron passiveNeuron = new PassiveNeuron(value);
                passiveNeurons.Add(passiveNeuron);
            }
            return passiveNeurons;
        }

        public static ExtendedContext getSample()
        {
            return new ExtendedContext(new List<int>(new int[] { 1, 0, 0, 1, 0, 1, 1, 1, 0, 1 }));
        }
    }
}
