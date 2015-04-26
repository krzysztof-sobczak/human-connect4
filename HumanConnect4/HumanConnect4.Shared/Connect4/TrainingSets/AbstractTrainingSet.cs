using HumanConnect4.NeuralNetwork.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4.TrainingSets
{
    public abstract class AbstractTrainingSet : HumanConnect4.NeuralNetwork.AbstractTrainingSet
    {
        protected const int NUMBER_OF_CONTEXTS = NeuralNetwork.NUMBER_OF_CONTEXTS;
        protected const int NUMBER_OF_FRAMES = NeuralNetwork.NUMBER_OF_FRAMES;
        protected const int NUMBER_OF_COLUMNS_TO_PLAY = NeuralNetwork.NUMBER_OF_COLUMNS_TO_PLAY;

        protected OutputLayer columnNumberToOutputLayer(int columnNumber)
        {
            List<float> outputValues = new List<float>();
            for (int i = 1; i <= NUMBER_OF_COLUMNS_TO_PLAY; i++)
            {
                if (columnNumber == i)
                {
                    outputValues.Add(1);
                }
                else
                {
                    outputValues.Add(0);
                }
            }
            return new OutputLayer(outputValues);
        }
    }
}
