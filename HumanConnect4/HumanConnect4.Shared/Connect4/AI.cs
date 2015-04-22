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
        private const int NUMBER_OF_CONTEXTS = 39;
        private const int NUMBER_OF_FRAMES = 3;
        private const int NUMBER_OF_FEATURE_DETECTORS = 1;
        private const int NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR = 3;
        private const int NUMBER_OF_NEURONS_IN_SECOND_HIDDEN_LAYER = 7;
        private const int NUMBER_OF_COLUMNS_TO_PLAY = 7;

        private Network neuralNetwork;

        private Network NeuralNetwork
        {
            get { return neuralNetwork; }
            set { neuralNetwork = value; }
        }

        public AI()
        {
            this.NeuralNetwork = createNetwork();

            List<InputLayer> inputLayers = new List<InputLayer>();
            inputLayers.Add(getInputLayer());

            List<OutputLayer> expectedOutputLayers = new List<OutputLayer>();
            expectedOutputLayers.Add(new OutputLayer(new List<float>(new float[] { 0, 1, 0, 0, 0, 0, 0 })));

            train(inputLayers, expectedOutputLayers);
        }

        public void train(List<InputLayer> inputLayers, List<OutputLayer> expectedOutputLayers)
        {
            NeuralNetwork.train(inputLayers, expectedOutputLayers);
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

        private Network createNetwork()
        {
            Network network = new Network();

            network.InputLayer = getInputLayer();

            ConvolutionLayer detectorsLayer = getDetectorsLayer(network.InputLayer);
            network.HiddenLayers.Add(detectorsLayer);

            ConvolutionLayer secondHiddenLayer = getSecondHiddenLayer(detectorsLayer);
            network.HiddenLayers.Add(secondHiddenLayer);

            network.OutputLayer = getOutputLayer(secondHiddenLayer);

            network.feedForward(network.InputLayer);

            return network;
        }

        private InputLayer getInputLayer()
        {
            InputLayer inputLayer = new InputLayer();

            for (int i = 0; i < NUMBER_OF_CONTEXTS; i++ )
            {
                ExtendedContext context = ExtendedContext.getSample();
                inputLayer.Neurons.AddRange(context.getPassiveNeurons());
            }

            return inputLayer;
        }

        private ConvolutionLayer getDetectorsLayer(InputLayer inputLayer)
        {
            Layer patternLayer = new Layer(NUMBER_OF_CONTEXTS * NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR * NUMBER_OF_FEATURE_DETECTORS);
            for (int i = 0; i < NUMBER_OF_CONTEXTS; i++)
            {
                List<AbstractNeuron> contextNeurons = new List<AbstractNeuron>();
                contextNeurons.AddRange(inputLayer.Neurons.GetRange(i * ExtendedContext.CONTEXT_LENGTH, ExtendedContext.CONTEXT_LENGTH));
                List<Neuron> contextDetectorNeurons = patternLayer.Neurons.GetRange(i * NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR, NUMBER_OF_OUTPUT_NEURONS_IN_DETECTOR);
                Edge.connectAllNeurons(contextDetectorNeurons, contextNeurons);
            }
            ConvolutionLayer detectorsLayer = new ConvolutionLayer(NUMBER_OF_CONTEXTS, patternLayer);

            return detectorsLayer;
        }

        private ConvolutionLayer getSecondHiddenLayer(ConvolutionLayer detectorsLayer)
        {
            ConvolutionLayer secondHiddenLayer = new ConvolutionLayer(NUMBER_OF_CONTEXTS, new Layer(NUMBER_OF_NEURONS_IN_SECOND_HIDDEN_LAYER), detectorsLayer);
            return secondHiddenLayer;
        }

        private OutputLayer getOutputLayer(ConvolutionLayer secondHiddenLayer)
        {
            OutputLayer outputLayer = new OutputLayer(NUMBER_OF_COLUMNS_TO_PLAY);
            foreach(Layer layer in secondHiddenLayer.Layers)
            {
                List<AbstractNeuron> sourceNeurons = new List<AbstractNeuron>();
                sourceNeurons.AddRange(layer.Neurons);
                Edge.connectAllNeurons(outputLayer.Neurons, sourceNeurons);
            }
            return outputLayer;
        }

    }
}
