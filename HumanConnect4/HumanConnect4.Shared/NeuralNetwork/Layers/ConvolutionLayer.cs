using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    class ConvolutionLayer : AbstractHiddenLayer
    {

        private List<Layer> layers;

        public List<Layer> Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        public ConvolutionLayer()
        {
            this.Layers = new List<Layer>();
        }

        public ConvolutionLayer(int numberOfSublayers)
        {
            this.Layers = new List<Layer>();
            for (int i = 0; i < numberOfSublayers; i++)
            {
                this.Layers.Add(new Layer());
            }
        }

        public ConvolutionLayer(int numberOfSublayers, Layer sublayerPattern)
        {
            this.Layers = new List<Layer>();
            for (int i = 0; i < numberOfSublayers; i++)
            {
                this.Layers.Add(sublayerPattern);
            }
        }

        public ConvolutionLayer(int numberOfSublayers, Layer sublayerPattern, ConvolutionLayer convolutionLayerToConnectWith)
        {
            this.Layers = new List<Layer>();
            Random random = new Random(100);
            if (numberOfSublayers == convolutionLayerToConnectWith.Layers.Count)
            {
                for (int i = 0; i < numberOfSublayers; i++)
                {
                    Layer layer = sublayerPattern;
                    foreach(Neuron neuron in layer.Neurons)
                    {
                        foreach(Neuron inputNeuron in convolutionLayerToConnectWith.Layers[i].Neurons) {
                            Edge edge = new Edge((float)random.NextDouble(), inputNeuron);
                            neuron.Edges.Add(edge);
                        }
                    }
                    this.Layers.Add(sublayerPattern);
                }
            }
            else
            {
                throw new Exception("Connected convolution layers must have the same number of sublayers.");
            }
        }
    }
}
