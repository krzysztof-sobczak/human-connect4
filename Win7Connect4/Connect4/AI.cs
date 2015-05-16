using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using HumanConnect4.Connect4.TrainingSets;
using HumanConnect4.Connect4.TestSets;
using System.Xml;
using HumanConnect4.NeuralNetwork;
using HumanConnect4.NeuralNetwork.Layers;
using HumanConnect4.NeuralNetwork.Neurons;
using System.IO;

namespace HumanConnect4.Connect4
{
    class AI
    {

        private NeuralNetwork neuralNetwork;

        private NeuralNetwork NeuralNetwork
        {
            get { return neuralNetwork; }
            set { neuralNetwork = value; }
        }

        public AI()
        {
            // Create network for moves 1-6
            // load from xml
            // NeuralNetwork.fromXml("network1.xml");
            Network network1 = NeuralNetwork.getPartialMovesNetwork();
            // train network
            HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet1 = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            network1.train(trainingSet1);
            // test network
            HumanConnect4.Connect4.TestSets.AbstractTestSet testSet1 = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            NeuralNetwork.test(network1, testSet1);
            // serialize
            //network1.saveToXml("network1.xml");


            //// Create network for moves 7-12
            //// load from xml
            //// NeuralNetwork.fromXml("network2.xml");
            //Network network2 = NeuralNetwork.getPartialMovesNetwork();
            //// train network
            //HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet2 = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            //network2.train(trainingSet2);
            //// test network
            //HumanConnect4.Connect4.TestSets.AbstractTestSet testSet2 = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            //NeuralNetwork.test(network2, testSet2);
            //// serialize
            ////network1.saveToXml("network2.xml");

            //// Create network for moves 13-18
            //// load from xml
            //// NeuralNetwork.fromXml("network3.xml");
            //Network network3 = NeuralNetwork.getPartialMovesNetwork();
            //// train network
            //HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet3 = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            //network3.train(trainingSet3);
            //// test network
            //HumanConnect4.Connect4.TestSets.AbstractTestSet testSet3 = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            //NeuralNetwork.test(network3, testSet3);
            //// serialize
            ////network1.saveToXml("network3.xml");

            //// Create network for moves 19-24
            //// load from xml
            //// NeuralNetwork.fromXml("network4.xml");
            //Network network4 = NeuralNetwork.getPartialMovesNetwork();
            //// train network
            //HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet4 = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            //network4.train(trainingSet4);
            //// test network
            //HumanConnect4.Connect4.TestSets.AbstractTestSet testSet4 = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            //NeuralNetwork.test(network4, testSet4);
            //// serialize
            ////network1.saveToXml("network4.xml");

            //// Create network connecting all partial networks
            //// load from xml
            //// NeuralNetwork.fromXml("network.xml");
            //NeuralNetwork = new NeuralNetwork(network1, network2, network3, network4);
            //// test network
            //HumanConnect4.Connect4.TrainingSets.AbstractTrainingSet trainingSet = TrainingSetFactory.Create<TrainingSets.VelenaCsvSeries>();
            //NeuralNetwork.train(trainingSet4);

            //HumanConnect4.Connect4.TestSets.AbstractTestSet testSet = TestSetFactory.Create<TestSets.VelenaCsvSeries>();
            //NeuralNetwork.test(NeuralNetwork, testSet);
            //// serialize
            ////network1.saveToXml("network.xml");
        }

    }
}
