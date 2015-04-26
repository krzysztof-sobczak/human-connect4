using System;
using System.Collections.Generic;
using System.Text;
using HumanConnect4.NeuralNetwork;
using HumanConnect4.NeuralNetwork.Neurons;
using HumanConnect4.NeuralNetwork.Layers;
using System.Diagnostics;

namespace HumanConnect4.Connect4
{
    class AI
    {

        private Network neuralNetwork;

        private Network NeuralNetwork
        {
            get { return neuralNetwork; }
            set { neuralNetwork = value; }
        }

        public AI()
        {
            this.NeuralNetwork = new NeuralNetwork();

            AbstractTrainingSet trainingSet = TrainingSetFactory.Create<TrainingSets.Sample>();

            NeuralNetwork.train(trainingSet);
        }

        public int getMove(InputLayer inputLayer)
        {
            NeuralNetwork.feedForward(inputLayer);
            int bestMoveIndex = 0;
            for (int i = 0; i < NeuralNetwork.OutputLayer.Neurons.Count; i++ )
            {
                if(NeuralNetwork.OutputLayer.Neurons[i].Output > NeuralNetwork.OutputLayer.Neurons[bestMoveIndex].Output)
                {
                    bestMoveIndex = i;
                }
            }
            int bestMove = bestMoveIndex + 1;
            return bestMove;
        }

    }
}
