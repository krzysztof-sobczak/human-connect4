using HumanConnect4.NeuralNetwork;
using HumanConnect4.NeuralNetwork.Layers;
using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HumanConnect4.Connect4
{
    public class NeuralNetwork : Network
    {
        public const int NUMBER_OF_CONTEXTS = 17;
        public const int NUMBER_OF_FRAMES = 4;
        public const int NUMBER_OF_COLUMNS_TO_PLAY = 7;

        private const int NUMBER_OF_FEATURE_DETECTORS = 1;
        private const int NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR = 3;
        private const int NUMBER_OF_NEURONS_IN_SECOND_HIDDEN_LAYER = 5;

        public NeuralNetwork()
        {
            this.InputLayer = getInputLayer();

            NetworksLayer networksLayer = getNetworksLayer(this.InputLayer);

            this.OutputLayer = getOutputLayer(networksLayer);
        }

        public NeuralNetwork(Network network)
        {
            this.InputLayer = network.InputLayer;
            this.HiddenLayers = network.HiddenLayers;
            this.OutputLayer = network.OutputLayer;
        }

        public NeuralNetwork(Network networkFrom1To6, Network networkFrom7To12, Network networkFrom13To18, Network networkFrom19To24)
        {
            this.InputLayer = getInputLayer();

            NetworksLayer networksLayer = getNetworksLayer(new Network[] { networkFrom1To6, networkFrom7To12, networkFrom13To18, networkFrom19To24 }, this.InputLayer);

            this.OutputLayer = getOutputLayer(networksLayer);
        }

        public static NeuralNetwork getNeuralNetworkfromXml(string filepath)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(NeuralNetwork), new Type[] { typeof(Network), typeof(OutputLayer), typeof(InputLayer), typeof(PassiveNeuron), typeof(Neuron), typeof(Edge), typeof(Layer), typeof(ConvolutionLayer) });
            var streamRead = new FileStream(filepath, FileMode.Open);
            return (NeuralNetwork)serializer.Deserialize(streamRead);
        }

        public static void test(Network network, AbstractTestSet testSet)
        {
            if (testSet.InputLayers.Count != testSet.OutputLayers.Count)
            {
                throw new Exception("Training set must contain the same number of elements inputLayers and expectedOutputLayers .");
            }
            float positiveResultCount = 0;
            float negativeResultCount = 0;
            int testInstancesCount = testSet.InputLayers.Count;
            for (int k = 0; k < testInstancesCount; k++)
            {
                int expectedResult = getColumnFromOutputLayer(testSet.OutputLayers[k]);
                int networkResult = getMove(network, testSet.InputLayers[k]);
                if (expectedResult == networkResult)
                {
                    positiveResultCount++;
                } else {
                    negativeResultCount++;
                }
                Console.WriteLine(String.Format("[Test {0}] Expected result: {1}, Network result: {2}", k, expectedResult, networkResult));
            }
            var networkAccuracy = Math.Round((positiveResultCount / (positiveResultCount + negativeResultCount)) * 100, 2);
            Console.WriteLine(String.Format("Network accuracy: {0}%", networkAccuracy));
        }

        public static int getMove(Network network, InputLayer inputLayer)
        {
            network.feedForward(inputLayer);
            return getColumnFromOutputLayer(network.OutputLayer);
        }
        
        private static int getColumnFromOutputLayer(OutputLayer outputLayer)
        {
            int bestMoveIndex = 0;
            for (int i = 0; i < outputLayer.Neurons.Count; i++)
            {
                if (outputLayer.Neurons[i].Output > outputLayer.Neurons[bestMoveIndex].Output)
                {
                    bestMoveIndex = i;
                }
            }
            int bestMove = bestMoveIndex + 1;
            return bestMove;
        }

        private static InputLayer getInputLayer()
        {
            InputLayer inputLayer = new InputLayer();

            for (int i = 0; i < NUMBER_OF_FRAMES * NUMBER_OF_CONTEXTS; i++)
            {
                ExtendedContext context = ExtendedContext.getSample();
                inputLayer.Neurons.AddRange(context.getPassiveNeurons());
            }

            return inputLayer;
        }

        private static ConvolutionLayer getDetectorsLayer(InputLayer inputLayer)
        {
            Layer patternLayer = new Layer(NUMBER_OF_CONTEXTS * NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR * NUMBER_OF_FEATURE_DETECTORS);
            ConvolutionLayer detectorsLayer = new ConvolutionLayer(NUMBER_OF_FRAMES, patternLayer);
            // layers number is equal to the number of frames
            for(int layerIndex = 0; layerIndex < detectorsLayer.Layers.Count; layerIndex++)
            {
                for (int i = 0; i < NUMBER_OF_CONTEXTS; i++)
                {
                    List<AbstractNeuron> contextNeurons = new List<AbstractNeuron>();
                    contextNeurons.AddRange(inputLayer.Neurons.GetRange(layerIndex * NUMBER_OF_CONTEXTS * ExtendedContext.CONTEXT_LENGTH + i * ExtendedContext.CONTEXT_LENGTH, ExtendedContext.CONTEXT_LENGTH));
                    List<Neuron> contextDetectorNeurons = detectorsLayer.Layers[layerIndex].Neurons.GetRange(i * NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR, NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR);
                    Edge.connectAllNeurons(contextDetectorNeurons, contextNeurons);
                }
            }

            return detectorsLayer;
        }

        private static ConvolutionLayer getSecondHiddenLayer(ConvolutionLayer detectorsLayer)
        {
            Layer patternLayer = new Layer(NUMBER_OF_NEURONS_IN_SECOND_HIDDEN_LAYER);
            ConvolutionLayer secondHiddenLayer = new ConvolutionLayer(NUMBER_OF_FRAMES, patternLayer, detectorsLayer);
            return secondHiddenLayer;
        }

        public static Network getPartialMovesNetwork()
        {
            Network partialMovesNetwork = new Network();
            partialMovesNetwork.InputLayer = getInputLayer();

            ConvolutionLayer detectorsLayer = getDetectorsLayer(partialMovesNetwork.InputLayer);
            partialMovesNetwork.HiddenLayers.Add(detectorsLayer);

            ConvolutionLayer secondHiddenLayer = getSecondHiddenLayer(detectorsLayer);
            partialMovesNetwork.HiddenLayers.Add(secondHiddenLayer);

            partialMovesNetwork.OutputLayer = getOutputLayer(secondHiddenLayer);

            return partialMovesNetwork;
        }

        private static NetworksLayer getNetworksLayer(InputLayer inputLayer)
        {
            Network partialMovesNetwork = getPartialMovesNetwork();
            NetworksLayer networksLayer = new NetworksLayer(partialMovesNetwork, 4);
            networksLayer.assignInputLayer(inputLayer);
            return networksLayer;
        }

        private static NetworksLayer getNetworksLayer(Network[] networks, InputLayer inputLayer)
        {
            NetworksLayer networksLayer = new NetworksLayer(networks);
            networksLayer.assignInputLayer(inputLayer);
            return networksLayer;
        }

        private static OutputLayer getOutputLayer(AbstractHiddenLayer secondHiddenLayer)
        {
            OutputLayer outputLayer = new OutputLayer(NUMBER_OF_COLUMNS_TO_PLAY);
            Edge.connectAllNeurons(outputLayer.Neurons, secondHiddenLayer.getNeurons());
            return outputLayer;
        }

        public void saveNeuralNetworkToXml(string filepath)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(NeuralNetwork), new Type[] { typeof(Network), typeof(OutputLayer), typeof(InputLayer), typeof(PassiveNeuron), typeof(Neuron), typeof(Edge), typeof(Layer), typeof(ConvolutionLayer) });
            var streamWrite = new FileStream(filepath, FileMode.Create);
            serializer.Serialize(streamWrite, this);
            streamWrite.Close();
        }
    }
}
