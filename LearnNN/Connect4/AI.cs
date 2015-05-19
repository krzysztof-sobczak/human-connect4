using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using HumanConnect4.Connect4.TrainingSets;
using HumanConnect4.Connect4.TestSets;

namespace HumanConnect4.Connect4
{
    class AI
    {

        private NeuralNetwork neuralNetwork;

        public NeuralNetwork NeuralNetwork
        {
            get { return neuralNetwork; }
            set { neuralNetwork = value; }
        }

        public AI()
        {
            this.NeuralNetwork = new NeuralNetwork();

            AbstractTrainingSet trainingSet = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            NeuralNetwork.train(trainingSet);

            AbstractTestSet testSet = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            NeuralNetwork.test(testSet);

           
        }

    }
}
