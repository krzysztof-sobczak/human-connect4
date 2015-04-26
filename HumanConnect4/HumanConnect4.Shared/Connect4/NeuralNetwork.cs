using HumanConnect4.NeuralNetwork;
using HumanConnect4.NeuralNetwork.Layers;
using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
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

            ConvolutionLayer detectorsLayer = getDetectorsLayer(this.InputLayer);
            this.HiddenLayers.Add(detectorsLayer);

            ConvolutionLayer secondHiddenLayer = getSecondHiddenLayer(detectorsLayer);
            this.HiddenLayers.Add(secondHiddenLayer);

            this.OutputLayer = getOutputLayer(secondHiddenLayer);
        }

        private InputLayer getInputLayer()
        {
            InputLayer inputLayer = new InputLayer();

            for (int i = 0; i < NUMBER_OF_FRAMES * NUMBER_OF_CONTEXTS; i++)
            {
                ExtendedContext context = ExtendedContext.getSample();
                inputLayer.Neurons.AddRange(context.getPassiveNeurons());
            }

            return inputLayer;
        }

        private ConvolutionLayer getDetectorsLayer(InputLayer inputLayer)
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

        private ConvolutionLayer getSecondHiddenLayer(ConvolutionLayer detectorsLayer)
        {
            Layer patternLayer = new Layer(NUMBER_OF_NEURONS_IN_SECOND_HIDDEN_LAYER);
            ConvolutionLayer secondHiddenLayer = new ConvolutionLayer(NUMBER_OF_FRAMES, patternLayer, detectorsLayer);
            return secondHiddenLayer;
        }

        private OutputLayer getOutputLayer(ConvolutionLayer secondHiddenLayer)
        {
            OutputLayer outputLayer = new OutputLayer(NUMBER_OF_COLUMNS_TO_PLAY);
            foreach (Layer layer in secondHiddenLayer.Layers)
            {
                List<AbstractNeuron> sourceNeurons = new List<AbstractNeuron>();
                sourceNeurons.AddRange(layer.Neurons);
                Edge.connectAllNeurons(outputLayer.Neurons, sourceNeurons);
            }
            return outputLayer;
        }
    }
}
