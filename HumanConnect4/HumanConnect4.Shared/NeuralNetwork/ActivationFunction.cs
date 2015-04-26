using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork
{
    public class ActivationFunction
    {
        public static float sigmoid(float x)
        {
            return 1 / (1 + (float)Math.Exp(-(double)x));
        }

        public static float sigmoidDerivative(float x)
        {
            return (1 - x) * x;
        }

        public static float tanh(float x)
        {
            double positiveInfinity = Double.Parse("Infinity", System.Globalization.CultureInfo.InvariantCulture);
            double negativeInfinity = Double.Parse("-Infinity", System.Globalization.CultureInfo.InvariantCulture);
            if(x == positiveInfinity) {
                return 1;
            } else if( x == negativeInfinity) {
                return -1;
            } else {
                float y = (float)Math.Exp(2 * x);
                if (y != positiveInfinity) {
                    return (y - 1) / (y + 1);
                } else {
                    return 1;
                }
            }
        }

        public static float tanhDerivative(float x)
        {
            return 1 - (float)Math.Pow(x, 2);
        }
    }
}
